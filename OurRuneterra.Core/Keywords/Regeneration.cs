using OurRuneterra.Core.Cards;

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
  public override void OnInitialized(Game game, Card effectHolder) =>
    game.RegisterOnRoundEndedAction(effectHolder, () => OnRoundEnded(game, effectHolder));

  private static void OnRoundEnded(Game game, Card holder)
  {
    if (holder is Unit holdingUnit && game.GetCardLocation(holdingUnit) == CardLocation.Board)
      holdingUnit.CurrentHealth = holdingUnit.MaximumHealth;
  }
}