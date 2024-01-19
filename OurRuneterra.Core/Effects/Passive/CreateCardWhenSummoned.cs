using OurRuneterra.Core.Behaviours;
using OurRuneterra.Core.Cards;
using OurRuneterra.Core.Events;

namespace OurRuneterra.Core.Effects.Passive;

/// <summary>
/// The unit creates a card in the summoner's hand when it's summoned.
/// </summary>
public sealed class CreateCardWhenSummoned : PassiveEffect
{
  private readonly Card _createdCard;
  private Card? _effectHolder;

  /// <summary>
  /// Initializes a new instance of the <see cref="CreateCardWhenSummoned"/> class.
  /// </summary>
  /// <param name="createdCard">The card that will be created in the owner's hand when this card is summoned.</param>
  public CreateCardWhenSummoned(Card createdCard)
  {
    _createdCard = createdCard;
  }

  /// <inheritdoc/>
  public override void OnInitialized(Game game, Card card)
  {
    game.PlaceableSummoned += OnSummoned;
    _effectHolder = card;
  }

  private void OnSummoned(object? sender, PlaceableSummonedEventArgs args)
  {
    if (args.SummonedPlaceable != _effectHolder)
      return;

    args.Summoner.Hand.Add(_createdCard.Copy());
  }

  /// <inheritdoc/>
  public override void OnDestroyed(Game game, Card card)
  {
    game.PlaceableSummoned -= OnSummoned;
  }
}