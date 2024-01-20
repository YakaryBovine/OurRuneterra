using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Games;

namespace OurRuneterra.Riot;

/// <summary>
/// Provides all Foundations cards from the official Legends of Runeterra game by Riot.
/// </summary>
public static class FoundationsStartupExtensions
{
  public static GameStartupOptions AddFoundations(this GameStartupOptions options)
  {
    options.Cards.AddRange(new List<Card>
    {
      new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia)
      {
        Id = "01DE039",
        Rarity = CardRarity.Common,
        Subtypes = new List<CardSubtype>
        {
          CardSubtype.Elite
        }
      }
    });

    return options;
  }
}