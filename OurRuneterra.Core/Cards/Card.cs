using OurRuneterra.Core.Keywords;

namespace OurRuneterra.Core.Cards;

public abstract class Card
{
  public string Name { get; }
  
  public int Cost { get; }

  /// <summary>
  /// All <see cref="Keyword"/>s the unit currently has.
  /// </summary>
  public List<Keyword> Keywords = new();
  
  public Card(string name, int cost)
  {
    Name = name;
    Cost = cost;
  }
}