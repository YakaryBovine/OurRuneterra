using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Events;

namespace OurRuneterra.Core.Keywords;

/// <summary>
/// A <see cref="Keyword"/> that causes a <see cref="Unit"/> to take less damage.
/// </summary>
public sealed class Tough : Keyword
{
  /// <inheritdoc/>
  public override string Name => "Tough";
    
  /// <inheritdoc/>
  public override string Description => "Takes 1 less damage from all sources.";

  /// <inheritdoc/>
  public override void OnPlayed(Game game, Card card)
  {
    game.UnitDamaging += OnUnitDamaging;
    base.OnPlayed(game, card);
  }

  /// <inheritdoc/>
  public override void OnRemoved(Game game, Card card)
  {
    game.UnitDamaging -= OnUnitDamaging;
    base.OnRemoved(game, card);
  }

  private void OnUnitDamaging(object? sender, Damage damage)
  {
    if (damage.Victim == Holder && damage.Amount > 0)
      damage.Amount--;
  }
}