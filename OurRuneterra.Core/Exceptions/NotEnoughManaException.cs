using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

public sealed class NotEnoughManaException : Exception
{
  public NotEnoughManaException(Player placer, Card card) : base(
    $"{placer.Name} doesn't have enough mana to play {card.Name}.")
  {
    
  }
}