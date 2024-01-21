using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Games;
using OurRuneterra.Core.Keywords;

namespace OurRuneterra.Riot.StartupExtensions;

/// <summary>
/// Provides all Foundations cards from the official Legends of Runeterra game by Riot.
/// </summary>
public static class FoundationsStartupExtensions
{
  public static GameStartupOptions AddFoundations(this GameStartupOptions options)
  {
    var eliteSubtype = new CardSubtype(RiotCardSubtypes.Elite);
    var poroSubtype = new CardSubtype(RiotCardSubtypes.Poro);
    
    options.CardSubtypes.AddRange(new List<CardSubtype>
    {
      eliteSubtype,
      poroSubtype
    });
    
    options.Cards.AddRange(new List<Card>
    {
      new Unit("Cithria of Cloudfield", 2, 2, 1, Region.Demacia)
      {
        Id = "01DE039",
        Rarity = CardRarity.Common,
        Subtypes = new List<CardSubtype>
        {
          eliteSubtype
        }
      },
      new Unit("Plucky Poro", 1, 1, 1, Region.Demacia)
      {
        Id = "01DE049",
        Rarity = CardRarity.Common,
        Subtypes = new List<CardSubtype>
        {
          poroSubtype
        },
        PassiveEffects = new List<PassiveEffect>
        {
          new Tough()
        }
      }
    });

    return options;
  }
}