﻿namespace OurRuneterra.Core.Exceptions;

public sealed class NotEnoughManaException : Exception
{
  public NotEnoughManaException(Player placer, Placeable card) : base($"{placer.Name} doesn't have enough mana to play {card.Name}.")
  {
    
  }
}