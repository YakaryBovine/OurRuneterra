namespace OurRuneterra.Core.Players;

public sealed class PlayerDto
{
  /// <summary>
  /// The name by which the player can be referred to by other players.
  /// </summary>
  public required string Name { get; init; }
  
  /// <summary>
  /// A unique identifier.
  /// </summary>
  public required int Id { get; init; }

  /// <summary>
  /// The IDs for every card in the player's deck.
  /// </summary>
  public List<string> DeckCardIds { get; init; } = new();
}