using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Keywords;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests;

public sealed class KeywordTests
{
  [Fact]
  public void Tough_Unit_Takes_One_Less_Damage()
  {
    var game = Utils.StartSimpleGame();
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

  [Fact]
  public void Regeneration_Unit_Heals_At_End_Of_Round()
  {
    var game = Utils.StartSimpleGame();
    var regenerationUnit = new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia)
    {
      CurrentHealth = 1
    };
    regenerationUnit.PassiveEffects.Add(new Regeneration());
    var testPlayer = game.Players.First();
    testPlayer.Hand.Add(regenerationUnit);
    game.PlaceCard(testPlayer, regenerationUnit);
    
    game.EndRound(testPlayer);

    regenerationUnit.CurrentHealth.Should().Be(2);
  }
}