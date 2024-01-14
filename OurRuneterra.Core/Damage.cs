using OurRuneterra.Core.Behaviours;

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
  /// The thing doing the damaging.
  /// </summary>
  public required IDamager Damager { get; init; }
  
  /// <summary>
  /// The thing that will be damaged.
  /// </summary>
  public required IDamageable Victim { get; set; }
}