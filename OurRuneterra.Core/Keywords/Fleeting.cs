using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Keywords;

public sealed class Fleeting : Keyword
{
  public override string Name => "Fleeting";

  public override string Description => "Fleeting cards discard from hand when the round ends.";
  
  /// <inheritdoc/>
  public override void OnInitialized(Game game, Card effectHolder) =>
    game.RegisterOnRoundEndedAction(effectHolder, () => OnRoundEnded(game, effectHolder));

  private static void OnRoundEnded(Game game, Card holder)
  {
    if (game.GetCardLocation(holder) == CardLocation.Hand)
      game.Discard(holder);
  }
}