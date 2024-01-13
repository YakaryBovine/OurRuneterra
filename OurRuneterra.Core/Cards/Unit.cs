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
}