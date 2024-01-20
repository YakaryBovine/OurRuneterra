﻿using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core;

/// <summary>
/// An entire Legends of Runeterra game server, consisting of all cards that are valid for players to include
/// in their decks.
/// <para>Can be used to start and end <see cref="Match"/>es.</para>
/// </summary>
public sealed class Game
{
  private readonly Dictionary<string, Card> _cardsById;

  /// <summary>
  /// Initializes a new instance of the <see cref="Game"/> class.
  /// </summary>
  /// <param name="cards">All cards in the game. <see cref="Player"/>s can only have <see cref="Card"/>s in their deck
  /// if they were added here first.</param>
  public Game(IEnumerable<Card> cards)
  {
    _cardsById = cards.ToDictionary(x => x.Id);
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

  /// <summary>
  /// Returns if the card is a valid inclusion in a player's deck.
  /// </summary>
  /// <exception cref="InvalidCardRarityException">Thrown if the card is <see cref="CardRarity.Uncollectible"/>.</exception>
  private static void ValidateCard(Card card)
  {
    if (card.Rarity == CardRarity.Uncollectible)
      throw new InvalidCardRarityException(card);
  }
}