using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Exceptions;

public sealed class CantExceedMaximumHealthException : Exception
{
  public CantExceedMaximumHealthException(Unit unit, int currentHealth, int maximumHealth) : base(
    $"Can't set {unit.Name}'s current health to {currentHealth} because that would be higher than its maximum health {maximumHealth}.")
  {
  }
}