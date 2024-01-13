using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Test;

public sealed class GameTests
{
  [Fact]
  public void Player_With_Enough_Mana_Can_Place_Card()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0,
      MaximumManaGems = 10,
      CurrentManaGems = 10
    };
    game.Players.Add(testPlayer);
    var expensiveCard = new Unit("Expensive Card", 5, 5, 10, Region.Demacia)
    {
      CurrentHealth = 0
    };
    game.PlaceCard(testPlayer, expensiveCard);
    game.PlacedCards.Should().Contain(expensiveCard);
  }

  [Fact]
  public void Player_With_Insufficient_Mana_Cannot_Place_Card()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0,
      MaximumManaGems = 1,
      CurrentManaGems = 1
    };
    game.Players.Add(testPlayer);
    var expensiveCard = new Unit("Expensive Card", 5, 5, 10, Region.Demacia)
    {
      CurrentHealth = 0
    };
    game.Invoking(x => x.PlaceCard(testPlayer, expensiveCard))
      .Should()
      .Throw<NotEnoughManaException>();
  }

  [Fact]
  public void Round_Starting_Causes_Players_To_Refill_Mana_Gems()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0,
      MaximumManaGems = 5,
      CurrentManaGems = 0,
      Deck = { new Unit("TestCard", 1, 1, 1, Region.Demacia) }
    };
    game.Players.Add(testPlayer);
    game.StartNewRound();
    testPlayer.CurrentManaGems.Should().Be(testPlayer.MaximumManaGems);
  }
  
  [Fact]
  public void Round_Starting_Causes_Players_To_Gain_A_Mana_Gem()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0,
      MaximumManaGems = 5,
      CurrentManaGems = 0,
      Deck = { new Unit("TestCard", 1, 1, 1, Region.Demacia) }
    };
    game.Players.Add(testPlayer);
    game.StartNewRound();
    testPlayer.CurrentManaGems.Should().Be(6);
  }
  
  [Fact]
  public void Round_Starting_Causes_Players_To_Draw_A_Card()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0,
      MaximumManaGems = 5,
      CurrentManaGems = 0,
      Deck = { new Unit("TestCard", 1, 1, 1, Region.Demacia) }
    };
    game.Players.Add(testPlayer);
    game.StartNewRound();
    testPlayer.Hand.Should().HaveCount(1);
  }
}