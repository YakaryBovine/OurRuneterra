using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Players;

namespace OurRuneterra.Core.Exceptions;

public sealed class NotEnoughManaException : Exception
{
  internal NotEnoughManaException(Player placer, Card card) : base(
    $"{placer.Name} doesn't have enough mana to play {card.Name}.")
  {
    
  }
}