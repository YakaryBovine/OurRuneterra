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
  public override void OnPlayed(Game game, Card card)
  {
    game.RoundEnded += OnRoundEnded;
    base.OnPlayed(game, card);
  }

  /// <inheritdoc/>
  public override void OnRemoved(Game game, Card card)
  {
    game.RoundEnded -= OnRoundEnded;
    base.OnRemoved(game, card);
  }

  private void OnRoundEnded(object? sender, EventArgs eventArgs)
  {
    if (Holder is Unit holdingUnit) 
      holdingUnit.CurrentHealth = holdingUnit.MaximumHealth;
  }
}