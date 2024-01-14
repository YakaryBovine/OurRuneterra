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
  public required SpellEffect Effect { get; init; }

  /// <summary>
  /// Casts the spell on a number of targets.
  /// </summary>
  public void Cast(Game game, Player player, Spell spell, List<IDamageable> targets)
  {
    foreach (var target in targets)
      if (!Effect.TargetCondition(target))
        throw new InvalidTargetException(target);
      
    Effect.Result(game, player, spell, targets);
  }
}