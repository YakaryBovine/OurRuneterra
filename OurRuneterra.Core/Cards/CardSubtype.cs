namespace OurRuneterra.Core.Cards;

/// <summary>
/// An additional descriptor for a card which is mechanically significant to other cards.
/// </summary>
public sealed class CardSubtype
{
  /// <summary>
  /// A unique identifier for the subtype, which the player can see.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="CardSubtype"/> class.
  /// </summary>
  /// <param name="name">A unique identifier for the subtype, which the player can see.</param>
  public CardSubtype(string name)
  {
    Name = name;
  }
}