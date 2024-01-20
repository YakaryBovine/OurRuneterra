using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

public sealed class InvalidCardIdException : Exception
{
  internal InvalidCardIdException(string cardId) : base(
    $"There is no card with ID {cardId} in the game.")
  {
  }
}