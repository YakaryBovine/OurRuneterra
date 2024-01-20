using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Keywords;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests.KeywordTests;

public sealed class FleetingTests
{
  [Fact]
  public void Fleeting_Card_In_Hand_Should_Discard_At_Round_End()
  {
    var game = Utils.StartSimpleGame();
    var testPlayer = game.Players.First();
    var fleetingCard = game.CreateInHand(testPlayer, new Unit("Cithria of Cloudfield", 2, 2, 0, Region.Demacia)
    {
      CurrentHealth = 1,
      PassiveEffects = new List<PassiveEffect>
      {
        new Fleeting()
      }
    });

    game.EndRound(testPlayer);

    game.GetCardLocation(fleetingCard).Should().Be(CardLocation.Nowhere);
  }
}