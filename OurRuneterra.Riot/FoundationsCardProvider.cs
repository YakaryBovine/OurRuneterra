using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Keywords;

namespace OurRuneterra.Riot;

/// <summary>
/// Provides all Foundations cards from the official Legends of Runeterra game by Riot.
/// </summary>
public static class FoundationsCardProvider
{
  public static IEnumerable<Card> GetAllFoundationsCards()
  {
    yield return new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia)
    {
      Id = "01DE039",
      Rarity = CardRarity.Common,
      Subtypes = new List<CardSubtype>
      {
        CardSubtype.Elite
      }
    };

    yield return new Unit("Garen", 5, 5, 5, Region.Demacia)
    {
      Id = "01DE012",
      Rarity = CardRarity.Champion,
      Subtypes = new List<CardSubtype>
      {
        CardSubtype.Elite
      },
      PassiveEffects = new List<PassiveEffect>
      {
        new Regeneration()
      }
    };
  }
}