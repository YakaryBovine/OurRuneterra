using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Matches;

namespace OurRuneterra.Core.Keywords;

/// <summary>
/// A <see cref="Keyword"/> that causes a <see cref="Unit"/> to regain its hit points at the end of a round.
/// </summary>
public sealed class Regeneration : Keyword
{
  /// <inheritdoc/>
  public override string Name => "Tough";
    
  /// <inheritdoc/>
  public override string Description => "Heals fully at the end of each round.";

  /// <inheritdoc/>
  public override void OnInitialized(Match match, Card effectHolder) =>
    match.RegisterOnRoundEndedAction(effectHolder, () => OnRoundEnded(match, effectHolder));

  private static void OnRoundEnded(Match match, Card holder)
  {
    if (holder is Unit holdingUnit && match.GetCardLocation(holdingUnit) == CardLocation.Board)
      holdingUnit.CurrentHealth = holdingUnit.MaximumHealth;
  }
}