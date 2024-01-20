using OurRuneterra.Core.Cards;

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
      Rarity = CardRarity.Common
    };
  }
}