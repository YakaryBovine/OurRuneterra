using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core;

/// <summary>
/// A single game of Legends of Runeterra, which ends when a player wins.
/// </summary>
public sealed class Game
{
  /// <summary>
  /// The participants of the game, who can play cards and interact with them.
  /// </summary>
  public List<Player> Players { get; } = new();

  /// <summary>
  /// Cards which have been placed on the game board.
  /// </summary>
  public List<Placeable> PlacedCards { get; } = new();

  /// <summary>
  /// Places a card on the board.
  /// </summary>
  public void PlaceCard(Player placer, Placeable card)
  {
    if (placer.CurrentMana < card.Cost)
      throw new NotEnoughManaException(placer, card);

    PlacedCards.Add(card);
  }
}