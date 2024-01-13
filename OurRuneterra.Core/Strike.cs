using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core;

/// <summary>
/// An amount of damage that will be dealt from one unit to another.
/// </summary>
public sealed class Strike
{
  /// <summary>
  /// The amount of damage that will be dealt.
  /// </summary>
  public required int Amount { get; set; }
  
  /// <summary>
  /// The unit doing the striking.
  /// </summary>
  public Unit Striker { get; set; }
  
  /// <summary>
  /// The unit that will be struck.
  /// </summary>
  public Unit Victim { get; set; }
}