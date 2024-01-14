using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Keywords;

namespace OurRuneterra.Core.Tests;

public sealed class KeywordTests
{
  [Fact]
  public void Tough_Unit_Takes_One_Less_Damage()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0
    };
    var damager = new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia);
    var victim = new Unit("Vanguard Defender", 2, 2, 0, Region.Demacia);
    victim.Keywords.Add(new Tough());
    testPlayer.Hand.Add(damager);
    testPlayer.Hand.Add(victim);
    game.PlaceCard(testPlayer, damager);
    game.PlaceCard(testPlayer, victim);
    
    game.Damage(damager, victim, 2);
    
    victim.CurrentHealth.Should().Be(1);
  }

  [Fact]
  public void Regeneration_Unit_Heals_At_End_Of_Round()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0
    };
    var regenerationUnit = new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia)
    {
      CurrentHealth = 1
    };
    regenerationUnit.Keywords.Add(new Regeneration());
    testPlayer.Hand.Add(regenerationUnit);
    game.PlaceCard(testPlayer, regenerationUnit);
    game.EndRound();

    regenerationUnit.CurrentHealth.Should().Be(2);
  }
}