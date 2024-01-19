using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Tests;

public sealed class CardTests
{
  [Fact]
  public void Cards_Cant_Be_Initialized_Twice()
  {
    var testCard = new Unit("TestCard", 1, 1, 1, Region.Demacia);
    var game = new Game();
    testCard.Initialize(game);

    testCard.Invoking(x => x.Initialize(game)).Should().Throw<CardAlreadyInitialized>();
  }
}