using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core;

/// <summary>
///   A single game of Legends of Runeterra, which ends when a player wins.
/// </summary>
public sealed class Game
{
  /// <summary>
  /// The state the game is currently in, which restricts the operations that can be called on it.
  /// </summary>
  public GameState State { get; set; } = GameState.NotStarted;
  
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
  
  /// <summary>
  ///   Invoked when a unit starts striking. The strike can be modified prior to completion.
  /// </summary>
  internal event EventHandler<Strike>? UnitStriking;

  /// <summary>
  ///   Invoked when a unit starts damaging. The damage can be modified prior to completion.
  /// </summary>
  internal event EventHandler<Damage>? UnitDamaging;

  /// <summary>
  ///   Invoked when a round ends.
  /// </summary>
  internal event EventHandler? RoundEnded;

  /// <summary>
  /// Starts the game, allowing it to be played.
  /// </summary>
  /// <param name="players">All players participating in the game.</param>
  public void Start(IEnumerable<Player> players)
  {
    ThrowIfNotState(GameState.NotStarted);
    
    Players.AddRange(players);
    foreach (var player in Players) 
      player.CurrentManaGems = 1;
    
    TurnPlayer = Players.First();

    State = GameState.InProgress;
  }

  /// <summary>
  ///   Ends the current round, then starts a new one.
  /// </summary>
  public void EndRound(Player endingPlayer)
  {
    ThrowIfNotState(GameState.InProgress);
    
    if (endingPlayer != TurnPlayer)
      throw new NotPlayersTurnException(endingPlayer);
    
    RoundEnded?.Invoke(this, EventArgs.Empty);
    StartRound();
  }

  /// <summary>
  ///   Places a card on the board.
  /// </summary>
  public void PlaceCard(Player placer, Placeable card)
  {
    ThrowIfNotState(GameState.InProgress);
    
    if (placer != TurnPlayer)
      throw new NotPlayersTurnException(placer);
    
    if (placer.CurrentManaGems < card.Cost)
      throw new NotEnoughManaException(placer, card);

    if (!placer.Hand.Contains(card))
      throw new MustPlayFromHandException(card);
    
    foreach (var keyword in card.Keywords)
      keyword.OnPlayed(this, card);

    placer.Hand.Remove(card);
    Board.Add(card);
  }

  /// <summary>
  /// Casts a spell.
  /// </summary>
  public void Cast(Player player, Spell spell, List<IDamageable> targets)
  {
    ThrowIfNotState(GameState.InProgress);
    
    if (player != TurnPlayer)
      throw new NotPlayersTurnException(player);
    
    if (player.CurrentManaGems < spell.Cost)
      throw new NotEnoughManaException(player, spell);
      
    spell.Cast(this, player, spell, targets);
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

    UnitStriking?.Invoke(this, strike);

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

    UnitDamaging?.Invoke(this, damage);

    victim.CurrentHealth -= damage.Amount;
    if (victim.CurrentHealth == 0)
      victim.Kill(damager);
  }
  
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

  private void ThrowIfNotState(GameState requiredState)
  {
    if (State != requiredState)
      throw new WrongGameStateException(requiredState, State);
  }
}