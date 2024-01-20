using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core;

/// <summary>
/// Someone who is playing a Legends of Runeterra <see cref="Match"/>.
/// </summary>
public sealed class Player
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
  /// How much mana the player has left this turn.
  /// </summary>
  public int CurrentManaGems { get; set; } = 1;

  /// <summary>
  /// How much mana the player can use each turn.
  /// </summary>
  public int MaximumManaGems { get; set; } = 1;

  /// <summary>
  /// The cards in the player's hand. The player can these cards during their turn.
  /// </summary>
  public List<Card> Hand { get; } = new();

  /// <summary>
  /// The cards in the player's deck. Each round the player adds one card to their hand from the top of their deck.
  /// </summary>
  public List<Card> Deck { get; init; } = new();

  /// <summary>
  /// A damageable entity that, when reduced to 0 hit points, defeats the player.
  /// </summary>
  public Nexus Nexus { get; } = new()
  {
    CurrentHealth = 20,
    MaximumHealth = 20
  };

  /// <summary>
  /// The player refills all of their mana gems. Mana gems can be used to play cards.
  /// </summary>
  public void RefillManaGems()
  {
    CurrentManaGems = MaximumManaGems;
  }

  /// <summary>
  /// The player adds one card from the top of their deck to their hand.
  /// </summary>
  public void Draw()
  {
    var topOfDeck = Deck.LastOrDefault();
    if (topOfDeck == null)
    {
      Defeat();
      return;
    }
    
    Deck.Remove(Deck.Last());
    Hand.Add(topOfDeck);
  }

  /// <summary>
  /// Defeats the player, removing them from the game.
  /// </summary>
  public void Defeat()
  {
    throw new NotImplementedException();
  }
}