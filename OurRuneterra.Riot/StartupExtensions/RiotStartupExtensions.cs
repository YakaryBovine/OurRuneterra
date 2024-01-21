using OurRuneterra.Core.Games;

namespace OurRuneterra.Riot.StartupExtensions;

/// <summary>
/// Provides all cards from the official Legends of Runeterra game by Riot.
/// </summary>
public static class RiotStartupExtensions
{
  public static GameStartupOptions AddRiot(this GameStartupOptions options)
  {
    return options
      .AddFoundations();
  }
}