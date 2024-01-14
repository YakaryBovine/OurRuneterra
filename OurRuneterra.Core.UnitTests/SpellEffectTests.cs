using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.SpellEffects;

namespace OurRuneterra.Core.Tests;

public sealed class SpellEffectTests
{
  [Fact]
  public void DealDamageToTarget_Deals_Damage_To_Target()
  {
    var game = new Game();
    var testPlayer = new Player
    {
      Name = "TestPlayer",
      Id = 0,
      MaximumManaGems = 5,
      CurrentManaGems = 5
    };
    game.Players.Add(testPlayer);
    var bladesEdge = new Spell("Blade's Edge", 1, Region.Noxus)
    {
      Effect = new DealDamageToTarget(1)
    };
    var victim = new Unit("Cithria", 2, 2, 2, Region.Demacia);
    
    game.Cast(testPlayer, bladesEdge, new List<IDamageable> { victim });
    
    victim.CurrentHealth.Should().Be(1);
  }
}