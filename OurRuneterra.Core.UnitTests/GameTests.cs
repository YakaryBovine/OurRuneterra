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
      {
        Id = "X"
      }
    });

    var testPlayers = new List<PlayerDto>
    {
      new()
      {
        Name = "TestPlayer",
        Id = 0,
        DeckCardIds = new List<string>
        {
          "Y"
        }
      }
    };
    
    game.Invoking(x => x.StartMatch(testPlayers)).Should().Throw<InvalidCardIdException>();
  }
  
  [Fact]
  public void Player_Can_Have_Card_That_Is_In_Game()
  {
    var game = new Game(new List<Card>
    {
      new Unit("Cithria of Cloudfield", 1, 1, 1, Region.Demacia)
      {
        Id = "X",
        Rarity = CardRarity.Common
      }
    });

    var testPlayers = new List<PlayerDto>
    {
      new()
      {
        Name = "TestPlayer",
        Id = 0,
        DeckCardIds = new List<string>
        {
          "X"
        }
      }
    };
    
    game.Invoking(x => x.StartMatch(testPlayers)).Should().NotThrow();
  }
  
  [Fact]
  public void Player_Cant_Have_Uncollectible_Card()
  {
    var game = new Game(new List<Card>
    {
      new Unit("Cithria of Cloudfield", 1, 1, 1, Region.Demacia)
      {
        Id = "X",
        Rarity = CardRarity.Uncollectible
      }
    });

    var testPlayers = new List<PlayerDto>
    {
      new()
      {
        Name = "TestPlayer",
        Id = 0,
        DeckCardIds = new List<string>
        {
          "X"
        }
      }
    };
    
    game.Invoking(x => x.StartMatch(testPlayers)).Should().Throw<InvalidCardRarityException>();
  }
}