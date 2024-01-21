using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

public class InvalidCardSubtypeException : Exception
{
  internal InvalidCardSubtypeException(CardSubtype cardSubtype) : base(
    $"{cardSubtype.Name} isn't a valid subtype.")
  {
  }
}