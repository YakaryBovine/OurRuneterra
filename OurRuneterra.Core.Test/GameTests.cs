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
      MaximumMana = 10,
      CurrentMana = 10
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
      MaximumMana = 1,
      CurrentMana = 1
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
}