using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Cards;

/// <summary>
/// A <see cref="Placeable"/> that can attack, block, and be damaged.
/// </summary>
public sealed class Unit : Placeable
{
  private int _currentHealth;

  public Unit(string name, int power, int maximumHealth, int cost, Region region) : base(name, cost)
  {
    Power = power;
    MaximumHealth = maximumHealth;
    Region = region;
    CurrentHealth = maximumHealth;
  }

  public int Power { get; }

  public int MaximumHealth { get; }

  public Region Region { get; }

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

  /// <summary>
  /// Causes the <see cref="Unit"/> to deal damage to another <see cref="Unit"/> equal to its power.
  /// </summary>
  public void Strike(Unit target)
  {
    Damage(target, Power);
  }

  /// <summary>
  /// Causes the <see cref="Placeable"/> to damage another <see cref="Placeable"/>.
  /// </summary>
  public void Damage(Unit target, int amount)
  {
    target.CurrentHealth -= amount;
    if (target.CurrentHealth == 0)
    {
      target.Kill(this);
    }
  }
}