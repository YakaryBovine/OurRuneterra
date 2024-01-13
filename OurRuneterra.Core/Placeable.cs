namespace OurRuneterra.Core;

/// <summary>
/// A card that can be placed on the board.
/// </summary>
public abstract class Placeable : Card
{
  protected Placeable(int cost) : base(cost)
  {
  }
  
  /// <summary>
  /// Removes the <see cref="Placeable"/> from the board.
  /// </summary>
  public void Kill(Card killer)
  {
    throw new NotImplementedException();
  }
}