using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Keywords;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests.KeywordTests;

public sealed class RegenerationTests
{
  [Fact]
  public void Regeneration_Unit_On_Board_Heals_At_End_Of_Round()
  {
    var game = Utils.StartSimpleGame();
    var testPlayer = game.Players.First();
    var regenerationUnit = game.Summon(testPlayer, new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia)
    {
      CurrentHealth = 1,
      PassiveEffects = new List<PassiveEffect>
      {
        new Regeneration()
      }
    }) as Unit;

    game.EndRound(testPlayer);

    regenerationUnit!.CurrentHealth.Should().Be(2);
  }

  [Fact]
  public void Regeneration_Unit_In_Hand_Doesnt_Heal_At_End_Of_Round()
  {
    var regenerationUnit = new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia)
    {
      CurrentHealth = 1,
      PassiveEffects = new List<PassiveEffect>
      {
        new Regeneration()
      }
    };
    var game = Utils.StartSimpleGame();
    var testPlayer = game.Players.First();
    game.CreateInHand(testPlayer, regenerationUnit);

    game.EndRound(testPlayer);

    regenerationUnit.CurrentHealth.Should().Be(1);
  }
}