using OurRuneterra.Core.Matches;

namespace OurRuneterra.Core.Exceptions;

public sealed class WrongMatchStateException : Exception
{
  internal WrongMatchStateException(MatchState requiredMatchState, MatchState currentMatchState) : base(
    $"That action can only be taken when the match is in state {requiredMatchState.ToString()}, but it is in state {currentMatchState.ToString()}.")
  {
  }
}