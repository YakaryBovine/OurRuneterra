using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Tests;

public sealed class UnitTests
{
  [Fact]
  public void Striking_Reduces_Health_Equal_To_Strikers_Power()
  {
    var striker = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    var victim = new Unit("Vanguard Lookout", 1, 4, 2, Region.Demacia);
    striker.Strike(victim);
    victim.CurrentHealth.Should().Be(2);
  }
  
  [Fact]
  public void Damaging_Reduces_Health_Equal_To_Damage()
  {
    var damager = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    var victim = new Unit("Vanguard Lookout", 1, 4, 2, Region.Demacia);
    damager.Damage(victim, 1);
    victim.CurrentHealth.Should().Be(3);
  }

  [Fact]
  public void Current_Health_Cant_Exceed_Maximum_Health()
  {
    var testUnit = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    testUnit.Invoking(x => x.CurrentHealth = 3)
      .Should()
      .Throw<CantExceedMaximumHealthException>();
  }
}