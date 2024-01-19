using OurRuneterra.Core.Behaviours;

namespace OurRuneterra.Core.Keywords;

/// <summary>
/// A keyword that can be applied to a card, granting it additional effects.
/// </summary>
public abstract class Keyword : PassiveEffect
{
  /// <summary>
  /// A descriptive name.
  /// </summary>
  public abstract string Name { get; }

  /// <summary>
  /// Describes what the keyword does.
  /// </summary>
  public abstract string Description { get; }
}