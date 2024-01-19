using OurRuneterra.Core.Cards;

namespace OurRuneterra.Core.Behaviours;

/// <summary>
/// An effect a card has which operates entirely by itself, with no player intervention required.
/// </summary>
public abstract class PassiveEffect
{
  /// <summary>
  /// Invoked when a card with the <see cref="PassiveEffect"/> is initialized at the start of a game,
  /// or when the effect is added to a card mid-game.
  /// </summary>
  public abstract void OnInitialized(Game game, Card effectHolder);
}