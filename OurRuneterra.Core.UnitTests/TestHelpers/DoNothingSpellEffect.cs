using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Tests.TestHelpers;

/// <summary>
/// A <see cref="CastEffect"/> which does nothing.
/// </summary>
public sealed class DoNothingSpellEffect : CastEffect
{
  public override bool TargetCondition(IDamageable damageable) => true;

  public override void Result(Game game, Player player, Spell castedSpell, List<IDamageable> damageables)
  {
  }
}