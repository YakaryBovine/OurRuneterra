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

  /// <summary>
  /// Initializes a new instance of the <see cref="CreateCardWhenSummoned"/> class.
  /// </summary>
  /// <param name="createdCard">The card that will be created in the owner's hand when this card is summoned.</param>
  public CreateCardWhenSummoned(Card createdCard)
  {
    _createdCard = createdCard;
  }

  /// <inheritdoc/>
  public override void OnInitialized(Match match, Card effectHolder)
  {
    match.RegisterOnSummonedAction(effectHolder as Placeable, @params => OnSummoned(@params, effectHolder));
  }

  private void OnSummoned(PlaceableSummonedParams parameters, Card effectHolder)
  {
    if (parameters.SummonedPlaceable != effectHolder)
      return;

    parameters.Summoner.Hand.Add(_createdCard.Copy());
  }
}