using Interaction;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class DrinkingState : SpecialState
    {
        protected override void Start()
        {
            void ActivateThis() => active = true;
            void DeactivateThis() => active = false;
            DrinkingInteraction.BeforeInteraction += ActivateThis;
            DrinkingInteraction.AfterInterruptedInteraction += DeactivateThis;
            DrinkingInteraction.AfterSuccessfulInteraction += DeactivateThis;
            base.Start();
        }
    }
}