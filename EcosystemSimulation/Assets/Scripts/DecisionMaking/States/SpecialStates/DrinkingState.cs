using Interaction;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class DrinkingState : SpecialState
    {
        protected override void Start()
        {
            base.Start();
            DrinkingInteraction.BeforeInteraction += ActivateThis;
            DrinkingInteraction.AfterInterruptedInteraction += DeactivateThis;
            DrinkingInteraction.AfterSuccessfulInteraction += DeactivateThis;
        }
    }
}