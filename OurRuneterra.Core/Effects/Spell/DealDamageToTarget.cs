using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Effects.Spell;

/// <summary>
/// Deals damage to a target.
/// </summary>
public sealed class DealDamageToTarget : CastEffect
{
  private readonly int _damageAmount;

  public DealDamageToTarget(int damageAmount)
  {
    _damageAmount = damageAmount;
  }
  
  /// <inheritdoc/>
  public override bool TargetCondition(IDamageable target) => target is Unit;

  /// <inheritdoc/>
  public override void Result(Game game, Player player, Cards.Spell castedSpell, List<IDamageable> targets)
  {
    foreach (var target in targets)
    {
      game.Damage(castedSpell, target, _damageAmount);
    }
  }
}