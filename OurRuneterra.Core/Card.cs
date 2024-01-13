namespace OurRuneterra.Core;

public abstract class Card
{
  public int Cost { get; }

  public Card(int cost)
  {
    Cost = cost;
  }
}