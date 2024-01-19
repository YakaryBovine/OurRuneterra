using OurRuneterra.Core.Behaviours;

namespace OurRuneterra.Core.Cards;

public abstract class Card
{
  public string Name { get; }
  
  public int Cost { get; }

  /// <summary>
  /// A list of effects the card passively performs.
  /// </summary>
  public List<PassiveEffect> PassiveEffects { get; init; } = new();
  
  public Region Region { get; }

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
  internal void Initialize(Game game)
  {
    foreach (var passiveEffect in PassiveEffects)
      passiveEffect.OnInitialized(game, this);
  }
}