using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Exceptions;

namespace OurRuneterra.Core;

/// <summary>
///   A single game of Legends of Runeterra, which ends when a player wins.
/// </summary>
public sealed class Game
{
  /// <summary>
  ///   The participants of the game, who can play cards and interact with them.
  /// </summary>
  public List<Player> Players { get; } = new();

  /// <summary>
  ///   Cards which have been placed on the game board.
  /// </summary>
  public List<Placeable> PlacedCards { get; } = new();

  /// <summary>
  ///   Invoked when a unit starts striking. The strike can be modified prior to completion.
  /// </summary>
  public event EventHandler<Strike>? UnitStriking;

  /// <summary>
  ///   Invoked when a unit starts damaging. The damage can be modified prior to completion.
  /// </summary>
  public event EventHandler<Damage>? UnitDamaging;

  /// <summary>
  ///   Invoked when a round ends.
  /// </summary>
  public event EventHandler? RoundEnded;

  /// <summary>
  ///   Ends the current round, then starts a new one.
  /// </summary>
  public void EndRound()
  {
    RoundEnded?.Invoke(this, EventArgs.Empty);
    StartRound();
  }

  /// <summary>
  ///   Places a card on the board.
  /// </summary>
  public void PlaceCard(Player placer, Placeable card)
  {
    if (placer.CurrentManaGems < card.Cost)
      throw new NotEnoughManaException(placer, card);

    foreach (var keyword in card.Keywords)
      keyword.OnPlayed(this, card);

    PlacedCards.Add(card);
  }

  /// <summary>
  ///   Causes a <see cref="Unit" /> to strike another, dealing damage equal to the striker's power.
  /// </summary>
  public void Strike(Unit striker, Unit victim)
  {
    var strike = new Strike
    {
      Amount = striker.Power,
      Striker = striker,
      Victim = victim
    };

    UnitStriking?.Invoke(this, strike);

    Damage(strike.Striker, strike.Victim, strike.Amount);
  }

  /// <summary>
  ///   Causes a <see cref="Unit" /> to damage another <see cref="Unit" />.
  /// </summary>
  public void Damage(Unit damager, Unit victim, int amount)
  {
    var damage = new Damage
    {
      Amount = amount,
      Damager = damager,
      Victim = victim
    };

    UnitDamaging?.Invoke(this, damage);

    victim.CurrentHealth -= damage.Amount;
    if (victim.CurrentHealth == 0)
      victim.Kill(damager);
  }
  
  /// <summary>
  ///   Ends the current round and starts a new one, causing each player to draw a card, gain an additional mana gem, and
  ///   refill their mana gems.
  /// </summary>
  private void StartRound()
  {
    foreach (var player in Players)
    {
      player.MaximumManaGems++;
      player.RefillManaGems();
      player.Draw();
    }
  }
}