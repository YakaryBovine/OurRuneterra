using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Players;

namespace OurRuneterra.Core.Events;

public sealed class PlaceableSummonedParams
{
  public PlaceableSummonedParams(Placeable summonedPlaceable, Player summoner)
  {
    SummonedPlaceable = summonedPlaceable;
    Summoner = summoner;
  }

  public Placeable SummonedPlaceable { get; }

  public Player Summoner { get; }
}