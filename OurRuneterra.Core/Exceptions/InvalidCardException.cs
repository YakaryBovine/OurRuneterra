using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

public sealed class InvalidCardException : Exception
{
  internal InvalidCardException(Card card, string reason) : base(
    $"card {card.Name} isn't valid because {reason}.")
  {
  }
}