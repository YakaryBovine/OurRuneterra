namespace OurRuneterra.Core.Exceptions;

public sealed class WrongGameStateException : Exception
{
  internal WrongGameStateException(GameState requiredGameState, GameState currentGameState) : base(
    $"That action can only be taken when the game is in state {requiredGameState.ToString()}, but it is in state {currentGameState.ToString()}.")
  {
  }
}