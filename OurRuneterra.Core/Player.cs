namespace OurRuneterra.Core;

/// <summary>
/// Someone who is playing a Legends of Runeterra <see cref="Game"/>.
/// </summary>
public sealed class Player
{
  /// <summary>
  /// The name by which the player can be referred to by other players.
  /// </summary>
  public required string Name { get; set; }
  
  /// <summary>
  /// A unique identifier.
  /// </summary>
  public required int Id { get; init; }

  /// <summary>
  /// How much mana the player has left this turn.
  /// </summary>
  public int CurrentMana { get; set; } = 1;

  /// <summary>
  /// How much mana the player can use each turn.
  /// </summary>
  public int MaximumMana { get; set; } = 1;
}