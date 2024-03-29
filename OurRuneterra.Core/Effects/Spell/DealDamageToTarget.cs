﻿using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Matches;
using OurRuneterra.Core.Players;

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
  public override void Result(Match match, Player player, Cards.Spell castedSpell, List<IDamageable> targets)
  {
    foreach (var target in targets)
    {
      match.Damage(castedSpell, target, _damageAmount);
    }
  }
}