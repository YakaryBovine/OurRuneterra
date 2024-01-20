using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Matches;

namespace OurRuneterra.Core.Keywords;

public sealed class Fleeting : Keyword
{
  public override string Name => "Fleeting";

  public override string Description => "Fleeting cards discard from hand when the round ends.";
  
  /// <inheritdoc/>
  public override void OnInitialized(Match match, Card effectHolder) =>
    match.RegisterOnRoundEndedAction(effectHolder, () => OnRoundEnded(match, effectHolder));

  private static void OnRoundEnded(Match match, Card holder)
  {
    if (match.GetCardLocation(holder) == CardLocation.Hand)
      match.Discard(holder);
  }
}