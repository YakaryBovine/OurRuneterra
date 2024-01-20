using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Tests.TestHelpers;

/// <summary>
/// A <see cref="CastEffect"/> which does nothing.
/// </summary>
public sealed class DoNothingSpellEffect : CastEffect
{
  public override bool TargetCondition(IDamageable damageable) => true;

  public override void Result(Match match, Player player, Spell castedSpell, List<IDamageable> damageables)
  {
  }
}