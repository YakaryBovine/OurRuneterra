using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Keywords;

/// <summary>
/// A keyword that can be applied to a card, granting it additional effects.
/// </summary>
public abstract class Keyword
{
  /// <summary>
  /// A descriptive name.
  /// </summary>
  public abstract string Name { get; }

  /// <summary>
  /// Describes what the keyword does.
  /// </summary>
  public abstract string Description { get; }
  
  /// <summary>
  /// The card this keyword is currently on.
  /// </summary>
  public Card? Holder { get; private set; }

  /// <summary>
  /// Invoked when a card with the <see cref="Keyword"/> is played.
  /// </summary>
  public virtual void OnPlayed(Game game, Card card)
  {
    Holder = card;
  }

  /// <summary>
  /// Invoked when a card with the <see cref="Keyword"/> is removed from the board.
  /// </summary>
  public virtual void OnRemoved(Game game, Card card)
  {
    Holder = null;
  }
}