namespace OurRuneterra.Core.Cards;

public abstract class Card
{
  public string Name { get; }
  
  public int Cost { get; }

  public Card(string name, int cost)
  {
    Name = name;
    Cost = cost;
  }
}