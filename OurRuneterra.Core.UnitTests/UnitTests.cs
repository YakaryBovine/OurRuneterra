using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Tests;

public sealed class UnitTests
{
  [Fact]
  public void Current_Health_Cant_Exceed_Maximum_Health()
  {
    var testUnit = new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia);
    testUnit.Invoking(x => x.CurrentHealth = 3)
      .Should()
      .Throw<CantExceedMaximumHealthException>();
  }
}