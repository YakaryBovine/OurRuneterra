using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

public sealed class InvalidCardRarityException : Exception
{
  internal InvalidCardRarityException(Card card) : base(
    $"Card {card.Name} with rarity {card.Rarity.ToString()} is not valid for that operation.")
  {
  }
}