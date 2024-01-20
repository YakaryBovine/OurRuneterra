using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Tests;

public sealed class GameTests
{
  [Fact]
  public void Player_Cant_Have_Card_That_Isnt_In_Game()
  {
    var game = new Game(new List<Card>
    {
      new Unit("Cithria of Cloudfield", 1, 1, 1, Region.Demacia)
    });

    var testPlayers = new List<Player>
    {
      new()
      {
        Name = "TestPlayer",
        Id = 0,
        Deck = new List<Card>
        {
          new Unit("Beans", 1, 1, 1, Region.Demacia)
        }
      }
    };
    
    game.Invoking(x => x.StartMatch(testPlayers)).Should().Throw<InvalidCardException>();
  }
  
  [Fact]
  public void Player_Can_Have_Card_That_Is_In_Game()
  {
    var game = new Game(new List<Card>
    {
      new Unit("Cithria of Cloudfield", 1, 1, 1, Region.Demacia)
    });

    var testPlayers = new List<Player>
    {
      new()
      {
        Name = "TestPlayer",
        Id = 0,
        Deck = new List<Card>
        {
          new Unit("Cithria of Cloudfield", 1, 1, 1, Region.Demacia)
        }
      }
    };
    
    game.Invoking(x => x.StartMatch(testPlayers)).Should().NotThrow();
  }
}