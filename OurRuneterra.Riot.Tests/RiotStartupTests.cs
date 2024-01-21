using OurRuneterra.Core.Games;
using OurRuneterra.Riot.StartupExtensions;

namespace OurRuneterra.Riot.Tests;

public class RiotStartupTests
{
  [Fact]
  public void Riot_Startup_Does_Not_Throw()
  {
    var riotStartup = new GameStartupOptions().AddRiot();
    var newGame = () => new Game(riotStartup);
    newGame.Should().NotThrow();
  }
}