using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core;

/// <summary>
/// An amount of damage that will be dealt from one unit to another.
/// </summary>
public sealed class Damage
{
  /// <summary>
  /// The amount of damage that will be dealt.
  /// </summary>
  public required int Amount { get; set; }
  
  /// <summary>
  /// The unit doing the damaging.
  /// </summary>
  public Unit Damager { get; set; }
  
  /// <summary>
  /// The unit that will be damaged.
  /// </summary>
  public Unit Victim { get; set; }
}