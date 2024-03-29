﻿using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Events;
using OurRuneterra.Core.Exceptions;
using OurRuneterra.Core.Players;

namespace OurRuneterra.Core.Matches;

/// <summary>
///   A single game of Legends of Runeterra, which ends when a player wins.
/// </summary>
public sealed class Match
{
  /// <summary>
  /// The state the game is currently in, which restricts the operations that can be called on it.
  /// </summary>
  public MatchState State { get; private set; } = MatchState.NotStarted;
  
  /// <summary>
  /// The player whos turn it currently is. Only the turn player can take actions.
  /// </summary>
  public Player? TurnPlayer { get; private set; }
  
  /// <summary>
  ///   The participants of the game, who can play cards and interact with them.
  /// </summary>
  internal List<Player> Players { get; } = new();

  /// <summary>
  ///   Cards which have been placed on the game board.
  /// </summary>
  internal List<Placeable> Board { get; } = new();
    
  private readonly Dictionary<Placeable, Action<PlaceableSummonedParams>> _onSummonedActions = new();
  private readonly Dictionary<Card, Action> _onRoundEndedActions = new();
  private readonly Dictionary<Card, Action<Damage>> _onUnitTakingDamageActions = new();

  /// <summary>
  /// Initializes a new instance of the <see cref="Match"/> class.
  /// </summary>
  internal Match()
  {
  }
  
  /// <summary>
  /// Starts the game, allowing it to be played.
  /// </summary>
  /// <param name="players">All players participating in the game.</param>
  public void Start(IEnumerable<Player> players)
  {
    ThrowIfNotState(MatchState.NotStarted);
    
    Players.AddRange(players);
    foreach (var player in Players) 
      player.CurrentManaGems = 1;
    
    TurnPlayer = Players.First();
    InitializeCards();
    State = MatchState.InProgress;
  }

  /// <summary>
  ///   Ends the current round, then starts a new one.
  /// </summary>
  public void EndRound(Player endingPlayer)
  {
    ThrowIfNotState(MatchState.InProgress);
    
    if (endingPlayer != TurnPlayer)
      throw new NotPlayersTurnException(endingPlayer);
    
    foreach (var (_, roundedEndedAction) in _onRoundEndedActions)
      roundedEndedAction.Invoke();

    StartRound();
  }

  /// <summary>
  ///   Places a card on the board.
  /// </summary>
  public void PlaceCard(Player placer, Placeable card)
  {
    ThrowIfNotState(MatchState.InProgress);
    
    if (placer != TurnPlayer)
      throw new NotPlayersTurnException(placer);
    
    if (placer.CurrentManaGems < card.Cost)
      throw new NotEnoughManaException(placer, card);

    if (!placer.Hand.Contains(card))
      throw new MustPlayFromHandException(card);
      
    placer.Hand.Remove(card);
    Summon(placer, card);
  }

  /// <summary>
  /// Casts a spell.
  /// </summary>
  public void Cast(Player player, Spell spell, List<IDamageable> targets)
  {
    ThrowIfNotState(MatchState.InProgress);
    
    if (player != TurnPlayer)
      throw new NotPlayersTurnException(player);
    
    if (player.CurrentManaGems < spell.Cost)
      throw new NotEnoughManaException(player, spell);
      
    spell.Cast(this, player, spell, targets);
  }

  /// <summary>
  /// Summons a <see cref="Placeable"/> straight to the board.
  /// </summary>
  internal Placeable Summon(Player summoner, Placeable cardToSummon)
  {
    foreach (var (_, summonAction) in _onSummonedActions)
      summonAction.Invoke(new PlaceableSummonedParams(cardToSummon, summoner));
    
    Board.Add(cardToSummon);
    
    if (!cardToSummon.Initialized)
      cardToSummon.Initialize(this);

    return cardToSummon;
  }
  
  /// <summary>
  /// Creates a card directly in the player's hand.
  /// </summary>
  internal Card CreateInHand(Player player, Card card)
  {
    card.Initialize(this);
    player.Hand.Add(card);
    return card;
  }

  /// <summary>
  /// Discards a card from a player's hand.
  /// </summary>
  internal void Discard(Card card)
  {
    var owningPlayer = Players.FirstOrDefault(x => x.Hand.Contains(card));
    
    if (owningPlayer == null)
      throw new InvalidOperationException($"No player has {card.Name} in their hand.");

    owningPlayer.Hand.Remove(card);
    UninitializeCard(card);
  }
  
  /// <summary>
  ///   Causes a <see cref="Unit" /> to strike another, dealing damage equal to the striker's power.
  /// </summary>
  internal void Strike(Unit striker, Unit victim)
  {
    var strike = new Strike
    {
      Amount = striker.Power,
      Striker = striker,
      Victim = victim
    };

    Damage(strike.Striker, strike.Victim, strike.Amount);
  }

  /// <summary>
  ///   Causes a <see cref="Unit" /> to damage another <see cref="Unit" />.
  /// </summary>
  internal void Damage(IDamager damager, IDamageable victim, int amount)
  {
    var damage = new Damage
    {
      Amount = amount,
      Damager = damager,
      Victim = victim
    };

    foreach (var (_, unitTakingDamageAction) in _onUnitTakingDamageActions)
      unitTakingDamageAction.Invoke(damage);

    victim.CurrentHealth -= damage.Amount;
    if (victim.CurrentHealth == 0)
      victim.Kill(damager);
  }

  /// <summary>
  /// Indicates where a <see cref="Card"/> is in the game.
  /// </summary>
  internal CardLocation GetCardLocation(Card card)
  {
    if (Players.SelectMany(x => x.Deck).Contains(card))
      return CardLocation.Deck;
    
    if (Players.SelectMany(x => x.Hand).Contains(card))
      return CardLocation.Hand;

    if (Board.Contains(card))
      return CardLocation.Board;

    return CardLocation.Nowhere;
  }
  
  /// <summary>
  /// Registers an action to occur when a particular <see cref="Placeable"/> is placed on the board.
  /// </summary>
  internal void RegisterOnSummonedAction(Placeable placeable, Action<PlaceableSummonedParams> onSummoned) =>
    _onSummonedActions.Add(placeable, onSummoned);

  /// <summary>
  /// Registers an actions to occur for the provided card every time a round ends.
  /// </summary>
  internal void RegisterOnRoundEndedAction(Card card, Action action) => _onRoundEndedActions.Add(card, action);

  /// <summary>
  /// Registers an action to occur whenever the provided card is about to take damage.
  /// </summary>
  public void RegisterOnUnitTakingDamageAction(Unit effectHolder, Action<Damage> action) =>
    _onUnitTakingDamageActions.Add(effectHolder, action);
  
  /// <summary>
  ///   Ends the current round and starts a new one, causing each player to draw a card, gain an additional mana gem, and
  ///   refill their mana gems.
  /// </summary>
  private void StartRound()
  {
    foreach (var player in Players)
    {
      player.MaximumManaGems++;
      player.RefillManaGems();
      player.Draw();
    }
  }

  /// <summary>
  /// Uninitializes the card, permanently disabling its functionality in the game.
  /// </summary>
  private void UninitializeCard(Card card)
  {
    _onRoundEndedActions.Remove(card);
    _onUnitTakingDamageActions.Remove(card);
    
    if (card is Placeable placeable)
      _onSummonedActions.Remove(placeable);

    card.Initialized = false;
  }
  
  /// <summary>
  /// Initializes all cards in the game, enabling them to perform any setup logic they need to function.
  /// </summary>
  private void InitializeCards()
  {
    var allCards = Players.SelectMany(x => x.Deck);
    foreach (var card in allCards)
      card.Initialize(this);
  }
  
  private void ThrowIfNotState(MatchState requiredState)
  {
    if (State != requiredState)
      throw new WrongMatchStateException(requiredState, State);
  }
}