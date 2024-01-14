using OurRuneterra.Core.Behaviours;

namespace OurRuneterra.Core.Cards;

/// <summary>
/// A card that can be placed on the board.
/// </summary>
public abstract class Placeable : Card
{
  protected Placeable(string name, int cost, Region region) : base(name, cost, region)
  {
  }
  
  /// <summary>
  /// Removes the <see cref="Placeable"/> from the board.
  /// </summary>
  public void Kill(IDamager killer)
  {
    
  }
}