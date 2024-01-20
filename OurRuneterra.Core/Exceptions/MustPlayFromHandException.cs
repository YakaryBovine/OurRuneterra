using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Players;

namespace OurRuneterra.Core.Exceptions;

public sealed class MustPlayFromHandException : Exception
{
  internal MustPlayFromHandException(Card card) : base(
    $"{card.Name} can only be played from a {nameof(Player)}'s {nameof(Player.Hand)}.")
  {
  }
}