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
  
  public Region Region { get; }
  
  public Card(string name, int cost, Region region)
  {
    Name = name;
    Cost = cost;
    Region = region;
  }
}