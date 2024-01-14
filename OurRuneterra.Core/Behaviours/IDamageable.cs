namespace OurRuneterra.Core.Behaviours;

/// <summary>
/// Anything that can be damaged.
/// </summary>
public interface IDamageable
{
  public string Name { get; }
  
  public int CurrentHealth { get; set; }

  /// <summary>
  /// Removes the <see cref="IDamageable"/> from the board.
  /// </summary>
  public void Kill(IDamager killer);
}