using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Keywords;

namespace OurRuneterra.Core.Cards;

public abstract class Card
{
  public string Name { get; }
  
  public int Cost { get; }

  /// <summary>
  /// All <see cref="Keyword"/>s the unit currently has.
  /// </summary>
  public List<Keyword> Keywords { get; } = new();

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
  public abstract Card Copy();
}