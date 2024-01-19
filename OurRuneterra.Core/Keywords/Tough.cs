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
  public override void OnInitialized(Game game, Card effectHolder)
  {
    game.RegisterOnUnitTakingDamageAction(effectHolder as Unit, (Damage damage) => { OnUnitTakingDamage(effectHolder, damage); });
  }
  
  private static void OnUnitTakingDamage(Card effectHolder, Damage damage)
  {
    if (effectHolder is Unit effectHolderUnit && damage.Victim == effectHolderUnit && damage.Amount > 0)
      damage.Amount--;
  }
}