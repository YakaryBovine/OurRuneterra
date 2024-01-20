using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Keywords;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests.KeywordTests;

public sealed class ToughTests
{
  [Fact]
  public void Tough_Unit_Takes_One_Less_Damage()
  {
    var game = Utils.StartSimpleMatch();
    var testPlayer = game.Players.First();
    var damager = game.Summon(testPlayer, new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia)) as Unit;
    var victim = game.Summon(testPlayer, new Unit("Vanguard Defender", 2, 2, 0, Region.Demacia)
    {
      PassiveEffects = new List<PassiveEffect>
      {
        new Tough()
      }
    }) as Unit;

    game.Damage(damager!, victim!, 2);

    victim!.CurrentHealth.Should().Be(1);
  }
}