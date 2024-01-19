namespace OurRuneterra.Core.Exceptions;

public sealed class NotPlayersTurnException : Exception
{
  internal NotPlayersTurnException(Player player) : base(
    $"{player.Name} can't take that action when it's not their turn.")
  {
  }
}