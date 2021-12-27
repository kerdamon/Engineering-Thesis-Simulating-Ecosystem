using Interaction;
using UnityEngine;

namespace DecisionMaking.States.SpecialStates
{
    public class MatingState : SpecialState
    {
        // [SerializeField] private MatingInteraction matingInteraction;

        protected override void Start()
        {
            // void ActivateThis() => active = true;
            // void DeactivateThis() => active = false;
            // matingInteraction.BeforeInteraction += ActivateThis;
            // matingInteraction.AfterInterruptedInteraction += DeactivateThis;
            // matingInteraction.AfterSuccessfulInteraction += DeactivateThis;
            base.Start();
        }
    }
}