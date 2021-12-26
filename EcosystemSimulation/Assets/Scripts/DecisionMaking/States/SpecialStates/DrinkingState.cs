using System;
using Interaction.Interactions;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class DrinkingState : SpecialState
    {
        [SerializeField] private DrinkingInteraction drinkingInteraction;

        protected override void Start()
        {
            void ActivateThis() => active = true;
            void DeactivateThis() => active = false;
            drinkingInteraction.BeforeInteraction += ActivateThis;
            drinkingInteraction.AfterInterruptedInteraction += DeactivateThis;
            drinkingInteraction.AfterSuccessfulInteraction += DeactivateThis;
            base.Start();
        }
    }
}