using OurRuneterra.Core.Behaviours;

namespace OurRuneterra.Core.Players;

/// <summary>
/// The part of a player that can be damaged and killed. When a player's nexus is killed, they lose the game.
/// </summary>
public sealed class Nexus : IDamageable
{
  public string Name => "Nexus";

  public int CurrentHealth { get; set; }
  
  public int MaximumHealth { get; set; }
  
  public void Kill(IDamager killer)
  {
    throw new NotImplementedException();
  }
}