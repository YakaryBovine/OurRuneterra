using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Tests.TestHelpers;

public static class Utils
{
  /// <summary>
  /// Creates and starts a valid, playable <see cref="Game"/>.
  /// </summary>
  /// <param name="players">If provided, these will be the players in the game. If not, two players will be created.</param>
  public static Game StartSimpleGame(List<Player>? players = null)
  {
    var game = new Game();
    game.Start(players ?? new List<Player>
    {
      new()
      {
        Name = "TestPlayerA",
        Id = 0,
        Deck = CreateSimpleDeck()
      },
      new()
      {
        Name = "TestPlayerB",
        Id = 1,
        Deck = CreateSimpleDeck()
      }
    });
    return game;
  }

  private static List<Card> CreateSimpleDeck()
  {
    var deck = new List<Card>();
    for (var i = 0; i < 40; i++)
    {
      deck.Add(new Unit("TestUnit", 1, 1, 1, Region.Demacia));
    }

    return deck;
  }
}