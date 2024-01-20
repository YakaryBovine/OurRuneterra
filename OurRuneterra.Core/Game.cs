using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core;

/// <summary>
/// An entire Legends of Runeterra game server, consisting of all cards that are valid for players to include
/// in their decks.
/// <para>Can be used to start and end <see cref="Match"/>es.</para>
/// </summary>
public sealed class Game
{
  private readonly List<Card> _cards;

  /// <summary>
  /// Initializes a new instance of the <see cref="Game"/> class.
  /// </summary>
  /// <param name="cards">All cards in the game. <see cref="Player"/>s can only have <see cref="Card"/>s in their deck
  /// if they were added here first.</param>
  public Game(IEnumerable<Card> cards)
  {
    _cards = cards.ToList();
  }
  
  /// <summary>
  /// Starts a competitive multiplayer match between the provided players.
  /// </summary>
  public Match StartMatch(ICollection<Player> players)
  {
    foreach (var card in players.SelectMany(x => x.Deck))
      ValidateCard(card);
    
    var newMatch = new Match();
    newMatch.Start(players);
    return newMatch;
  }

  private void ValidateCard(Card card)
  {
    if (!_cards.Contains(card))
      throw new InvalidCardException(card, "it doesn't exist in this game.");
  }
}