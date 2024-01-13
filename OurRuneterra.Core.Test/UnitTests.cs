using FluentAssertions;

namespace OurRuneterra.Core.Test;

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
}