using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Behaviours;

public abstract class CastEffect
{
  /// <summary>
  /// The condition that needs to be fulfilled for a target of the spell to be valid.
  /// </summary>
  public abstract bool TargetCondition(IDamageable damageable);

  /// <summary>
  /// What happens when the spell is cast.
  /// </summary>
  public abstract void Result(Game game, Player player, Spell castedSpell, List<IDamageable> damageables);
}