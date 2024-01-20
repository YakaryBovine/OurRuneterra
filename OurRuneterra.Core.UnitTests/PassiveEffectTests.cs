using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Effects.Passive;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests;

public sealed class PassiveEffectTests
{
  [Fact]
  public void CreateCardWhenSummoned_Creates_A_Card_When_Holder_Is_Summoned()
  {
    var createDuskPetalDustOnSummon = new CreateCardWhenSummoned(new Spell("Duskpetal Dust", 1, Region.Targon)
    {
      CastEffect = new DoNothingSpellEffect()
    });
    
    var lunariDuskbringer = new Unit("Lunari Duskbringer", 2, 1, 0, Region.Targon)
    {
      PassiveEffects = new List<PassiveEffect>
      {
        createDuskPetalDustOnSummon
      }
    };

    var testPlayer = new Player
    {
      Name = "TestPlayerA",
      Id = 0,
      Deck = new List<Card>
      {
        lunariDuskbringer
      }
    };
    
    var game = Utils.StartSimpleMatch(new List<Player>
    {
      testPlayer
    });
    
    testPlayer.Hand.Add(lunariDuskbringer);
    
    game.PlaceCard(testPlayer, lunariDuskbringer);
      
    testPlayer.Hand.Should().Contain(x => x.Name == "Duskpetal Dust");
  }
}