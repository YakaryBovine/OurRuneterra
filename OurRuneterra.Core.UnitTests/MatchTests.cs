using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests;

public sealed class MatchTests
{
  [Fact]
  public void Player_With_Enough_Mana_Can_Place_Card()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    testPlayer.MaximumManaGems = 10;
    testPlayer.CurrentManaGems = 10;
    var expensiveCard = new Unit("Expensive Card", 5, 5, 10, Region.Demacia);
    testPlayer.Hand.Add(expensiveCard);
    
    game.PlaceCard(testPlayer, expensiveCard);
    
    game.Board.Should().Contain(expensiveCard);
  }

  [Fact]
  public void Placing_Card_Removes_Card_From_Hand()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    var testCard = new Unit("Testcard", 1, 1, 1, Region.Demacia);
    testPlayer.Hand.Add(testCard);
    
    game.PlaceCard(testPlayer, testCard);

    testPlayer.Hand.Should().NotContain(testCard);
  }

  [Fact]
  public void Cards_Cant_Be_Placed_From_Outside_Hand()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    var testCard = new Unit("Testcard", 1, 1, 1, Region.Demacia);

    game.Invoking(x => x.PlaceCard(testPlayer, testCard)).Should().Throw<MustPlayFromHandException>();
  }

  [Fact]
  public void Player_With_Insufficient_Mana_Cannot_Place_Card()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    var expensiveCard = new Unit("Expensive Card", 5, 5, 10, Region.Demacia);
    
    game.Invoking(x => x.PlaceCard(testPlayer, expensiveCard))
      .Should()
      .Throw<NotEnoughManaException>();
  }

  [Fact]
  public void Round_Starting_Causes_Players_To_Refill_Mana_Gems()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    testPlayer.MaximumManaGems = 5;
    testPlayer.Deck.Add(new Unit("TestCard", 1, 1, 1, Region.Demacia));
    
    game.EndRound(testPlayer);
    
    testPlayer.CurrentManaGems.Should().Be(testPlayer.MaximumManaGems);
  }
  
  [Fact]
  public void Round_Starting_Causes_Players_To_Gain_A_Mana_Gem()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    testPlayer.MaximumManaGems = 5;
    
    game.EndRound(testPlayer);
    
    testPlayer.CurrentManaGems.Should().Be(6);
  }
  
  [Fact]
  public void Round_Starting_Causes_Players_To_Draw_A_Card()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    testPlayer.MaximumManaGems = 5;
    testPlayer.Deck.Add(new Unit("TestCard", 1, 1, 1, Region.Demacia));

    game.EndRound(testPlayer);
    
    testPlayer.Hand.Should().HaveCount(1);
  }
  
  [Fact]
  public void Striking_Reduces_Health_Equal_To_Strikers_Power()
  {
    var game = new Match();
    var striker = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    var victim = new Unit("Vanguard Lookout", 1, 4, 2, Region.Demacia);
    
    game.Strike(striker, victim);
    
    victim.CurrentHealth.Should().Be(2);
  }
  
  [Fact]
  public void Damaging_Reduces_Health_Equal_To_Damage()
  {
    var game = new Match();
    var damager = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    var victim = new Unit("Vanguard Lookout", 1, 4, 2, Region.Demacia);
    
    game.Damage(damager, victim, 1);
    
    victim.CurrentHealth.Should().Be(3);
  }
}