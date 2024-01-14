using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Tests;

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
    var expensiveCard = new Unit("Expensive Card", 5, 5, 10, Region.Demacia);
    testPlayer.Hand.Add(expensiveCard);
    game.PlaceCard(testPlayer, expensiveCard);
    game.Board.Should().Contain(expensiveCard);
  }

  [Fact]
  public void Placing_Card_Removes_Card_From_Hand()
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
    var testCard = new Unit("Testcard", 1, 1, 1, Region.Demacia);
    testPlayer.Hand.Add(testCard);
    game.PlaceCard(testPlayer, testCard);

    testPlayer.Hand.Should().NotContain(testCard);
  }

  [Fact]
  public void Cards_Cant_Be_Placed_From_Outside_Hand()
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
    var testCard = new Unit("Testcard", 1, 1, 1, Region.Demacia);

    game.Invoking(x => x.PlaceCard(testPlayer, testCard)).Should().Throw<MustPlayFromHandException>();
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
    game.EndRound();
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
    game.EndRound();
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
    game.EndRound();
    testPlayer.Hand.Should().HaveCount(1);
  }
  
  [Fact]
  public void Striking_Reduces_Health_Equal_To_Strikers_Power()
  {
    var game = new Game();
    var striker = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    var victim = new Unit("Vanguard Lookout", 1, 4, 2, Region.Demacia);
    
    game.Strike(striker, victim);
    
    victim.CurrentHealth.Should().Be(2);
  }
  
  [Fact]
  public void Damaging_Reduces_Health_Equal_To_Damage()
  {
    var game = new Game();
    var damager = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    var victim = new Unit("Vanguard Lookout", 1, 4, 2, Region.Demacia);
    
    game.Damage(damager, victim, 1);
    
    victim.CurrentHealth.Should().Be(3);
  }
}