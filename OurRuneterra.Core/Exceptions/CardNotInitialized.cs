using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

internal sealed class CardNotInitialized : Exception
{
  internal CardNotInitialized(Card card) : base(
    $"{card.Name} hasn't been initialized yet.")
  {
  }
}