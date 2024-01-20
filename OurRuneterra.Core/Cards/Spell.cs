using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Cards;

/// <summary>
/// A <see cref="Card"/> that can be cast from a player's hand, resulting in some kind of effect.
/// </summary>
public sealed class Spell : Card, IDamager
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Spell"/> class.
  /// </summary>
  /// <param name="name">The name of the spell that players see.</param>
  /// <param name="cost">How much mana is spent to cast the spell.</param>
  /// <param name="region">Where the spell is from.</param>
  public Spell(string name, int cost, Region region) : base(name, cost, region)
  {
  }
  
  /// <summary>
  /// Defines the behaviour of the spell.
  /// </summary>
  public required CastEffect CastEffect { get; init; }

  /// <summary>
  /// Casts the spell on a number of targets.
  /// </summary>
  public void Cast(Match match, Player player, Spell spell, List<IDamageable> targets)
  {
    foreach (var target in targets)
      if (!CastEffect.TargetCondition(target))
        throw new InvalidTargetException(target);
      
    CastEffect.Result(match, player, spell, targets);
  }

  /// <inheritdoc/>
  internal override Spell Copy()
  {
    return new Spell(Name, Cost, Region)
    {
      PassiveEffects = PassiveEffects,
      CastEffect = CastEffect,
      Id = Id
    };
  }
}