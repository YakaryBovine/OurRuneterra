using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Games;

namespace OurRuneterra.Riot;

/// <summary>
/// Provides all cards from the official Legends of Runeterra game by Riot.
/// </summary>
public static class RiotStartupExtensions
{
  public static GameStartupOptions AddRiot(this GameStartupOptions options, List<CardSet> sets)
  {
    if (sets.Contains(CardSet.Foundations))
      options.AddFoundations();

    return options;
  }
}