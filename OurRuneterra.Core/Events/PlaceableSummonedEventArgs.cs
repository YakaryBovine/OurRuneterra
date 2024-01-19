using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Events;

public sealed class PlaceableSummonedEventArgs : System.EventArgs
{
  public PlaceableSummonedEventArgs(Placeable summonedPlaceable, Player summoner)
  {
    SummonedPlaceable = summonedPlaceable;
    Summoner = summoner;
  }

  public Placeable SummonedPlaceable { get; }
  
  public Player Summoner { get; }
}