﻿using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;
using OurRuneterra.Core.Matches;
using OurRuneterra.Core.Players;

namespace OurRuneterra.Core.Games;

/// <summary>
/// An entire Legends of Runeterra game server, consisting of all cards that are valid for players to include
/// in their decks.
/// <para>Can be used to start and end <see cref="Match"/>es.</para>
/// </summary>
public sealed class Game
{
  private readonly Dictionary<string, Card> _cardsById;
  private readonly Dictionary<string, CardSubtype> _cardSubtypesById;

  /// <summary>
  /// Initializes a new instance of the <see cref="Game"/> class.
  /// </summary>
  /// <param name="options">All options used to configure the game.</param>
  public Game(GameStartupOptions options)
  {
    _cardsById = options.Cards.ToDictionary(x => x.Id);
    _cardSubtypesById = options.CardSubtypes.ToDictionary(x => x.Name);
  }

  /// <summary>
  /// Starts a competitive multiplayer match between the provided players.
  /// </summary>
  public Match StartMatch(IEnumerable<PlayerDto> players)
  {
    var newMatch = new Match();
    newMatch.Start(players.Select(PlayerDtoToPlayer));
    return newMatch;
  }

  private Player PlayerDtoToPlayer(PlayerDto playerDto)
  {
    return new Player
    {
      Name = playerDto.Name,
      Id = playerDto.Id,
      Deck = playerDto.DeckCardIds.Select(GetCardFromId).ToList()
    };
  }

  private Card GetCardFromId(string cardId)
  {
    if (!_cardsById.TryGetValue(cardId, out var card))
      throw new InvalidCardIdException(cardId);

    ValidateCard(card);

    return card;
  }
  
  private CardSubtype GetCardSubtypeFromId(string cardSubtypeId)
  {
    if (!_cardSubtypesById.TryGetValue(cardSubtypeId, out var cardSubtype))
      throw new InvalidCardIdException(cardSubtypeId);

    return cardSubtype;
  }

  /// <summary>
  /// Returns if the card is a valid inclusion in a player's deck.
  /// </summary>
  /// <exception cref="InvalidCardRarityException">Thrown if the card is <see cref="CardRarity.Uncollectible"/>.</exception>
  private void ValidateCard(Card card)
  {
    if (card.Rarity == CardRarity.Uncollectible)
      throw new InvalidCardRarityException(card);
    
    foreach (var cardSubtype in card.Subtypes)
      if (!_cardSubtypesById.ContainsKey(cardSubtype.Name))
        throw new InvalidCardSubtypeException(cardSubtype);
  }
}