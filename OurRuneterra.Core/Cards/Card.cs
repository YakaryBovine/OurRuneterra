using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core.Cards;

public abstract class Card
{
  /// <summary>
  /// A flavourful name for the card, which players can read.
  /// </summary>
  public string Name { get; }
  
  /// <summary>
  /// How much mana the card costs to play.
  /// </summary>
  public int Cost { get; }
  
  /// <summary>
  /// Where the card is from, which restricts the kinds of mechanics it should have.
  /// </summary>
  public Region Region { get; }

  /// <summary>
  /// A unique identifier.
  /// </summary>
  public string Id { get; init; } = "";

  public CardRarity Rarity { get; init; } = CardRarity.Uncollectible;
  
  /// <summary>
  /// Flavourful but mechanically significant extra types this card has.
  /// </summary>
  public List<CardSubtype> Subtypes { get; set; }
  
  /// <summary>
  /// A list of effects the card passively performs.
  /// </summary>
  public List<PassiveEffect> PassiveEffects { get; init; } = new();
  
  /// <summary>If true, the card has been initialized and is ready to be used in a game.</summary>
  public bool Initialized { get; internal set; }

  protected Card(string name, int cost, Region region)
  {
    Name = name;
    Cost = cost;
    Region = region;
  }

  /// <summary>
  /// Creates an exact copy of this card.
  /// </summary>
  internal abstract Card Copy();

  /// <summary>
  /// Initialize the card, allowing it to perform any initialization logic it needs.
  /// <para>Should only be run once, and only when the game starts.</para>
  /// </summary>
  internal void Initialize(Match match)
  {
    if (Initialized)
      throw new CardAlreadyInitialized(this);
    
    foreach (var passiveEffect in PassiveEffects)
      passiveEffect.OnInitialized(match, this);

    Initialized = true;
  }
}