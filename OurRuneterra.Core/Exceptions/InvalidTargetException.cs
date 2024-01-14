using OurRuneterra.Core.Behaviours;

namespace OurRuneterra.Core.Exceptions;

public sealed class InvalidTargetException : Exception
{
  internal InvalidTargetException(IDamageable target) : base(
    $"{target.Name} isn't a valid target for that effect.")
  {
  }
}