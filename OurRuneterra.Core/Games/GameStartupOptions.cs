using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Games;

/// <summary>
/// A set of startup options used to configure a new <see cref="Game"/>.
/// </summary>
public sealed class GameStartupOptions
{
  /// <summary>
  /// All cards that should exist in the game.
  /// </summary>
  public List<Card> Cards { get; init; } = new();

  /// <summary>
  /// All subtypes that should exist in the game.
  /// </summary>
  public List<CardSubtype> CardSubtypes { get; init; } = new();
}