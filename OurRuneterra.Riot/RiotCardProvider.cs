using OurRuneterra.Core.Cards;

namespace OurRuneterra.Riot;

/// <summary>
/// Provides all cards from the official Legends of Runeterra game by Riot.
/// </summary>
public static class RiotCardProvider
{
  public static IEnumerable<Card> GetAllRiotCards(ICollection<CardSet> sets)
  {
    if (sets.Contains(CardSet.Foundations))
      foreach (var card in FoundationsCardProvider.GetAllFoundationsCards())
        yield return card;
  }
}