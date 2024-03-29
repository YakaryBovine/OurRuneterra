﻿using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Effects.Spell;
using OurRuneterra.Core.Tests.TestHelpers;

namespace OurRuneterra.Core.Tests;

public sealed class CastEffectTests
{
  [Fact]
  public void DealDamageToTarget_Deals_Damage_To_Target()
  {
    var game = Utils.StartSimpleMatch();
    var bladesEdge = new Spell("Blade's Edge", 1, Region.Noxus)
    {
      CastEffect = new DealDamageToTarget(1)
    };
    var victim = new Unit("Cithria", 2, 2, 2, Region.Demacia);
    
    game.Cast(game.Players.First(), bladesEdge, new List<IDamageable> { victim });
    
    victim.CurrentHealth.Should().Be(1);
  }
}