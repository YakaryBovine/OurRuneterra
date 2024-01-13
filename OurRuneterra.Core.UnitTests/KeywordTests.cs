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
    game.PlaceCard(testPlayer, damager);
    game.PlaceCard(testPlayer, victim);
    
    game.Damage(damager, victim, 2);
    
    victim.CurrentHealth.Should().Be(1);
  }
}