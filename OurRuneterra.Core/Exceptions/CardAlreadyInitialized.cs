using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

internal sealed class CardAlreadyInitialized : Exception
{
  internal CardAlreadyInitialized(Card card) : base(
    $"{card.Name} has already been initialized.")
  {
  }
}