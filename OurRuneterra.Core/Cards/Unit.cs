using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Cards;

/// <summary>
/// A <see cref="Placeable"/> that can attack, block, and be damaged.
/// </summary>
public sealed class Unit : Placeable, IDamageable, IDamager
{
  private int _currentHealth;

  public Unit(string name, int power, int maximumHealth, int cost, Region region) : base(name, cost, region)
  {
    Power = power;
    MaximumHealth = maximumHealth;
    CurrentHealth = maximumHealth;
  }

  public int Power { get; }

  public int MaximumHealth { get; }

  public int CurrentHealth
  {
    get => _currentHealth;
    set
    {
      if (value > MaximumHealth)
        throw new CantExceedMaximumHealthException(this, value, MaximumHealth);
      
      _currentHealth = value;
    }
  }

  /// <inheritdoc/>
  internal override Unit Copy()
  {
    return new Unit(Name, Power, MaximumHealth, Cost, Region)
    {
      PassiveEffects = PassiveEffects,
      CurrentHealth = CurrentHealth
    };
  }
}