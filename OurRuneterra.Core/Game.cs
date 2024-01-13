using OurRuneterra.Core.Cards;
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
    if (placer.CurrentManaGems < card.Cost)
      throw new NotEnoughManaException(placer, card);

    PlacedCards.Add(card);
  }

  /// <summary>
  /// Ends the current round and starts a new one, causing each player to draw a card, gain an additional mana gem, and refill their mana gems.
  /// </summary>
  public void StartNewRound()
  {
    foreach (var player in Players)
    {
      player.MaximumManaGems++;
      player.RefillManaGems();
      player.Draw();
    }
  }
}