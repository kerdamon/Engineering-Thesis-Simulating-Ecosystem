using UnityEngine;

namespace DecisionMaking.States
{
    public class EatingState : InteractionState
    {
        public override void Start()
        {
            base.Start();
            var parent = transform.parent;
            PreviousState = parent.gameObject.GetComponentInChildren<HeadingForFoodState>();
            NextState = parent.gameObject.GetComponentInChildren<ChillingState>(); 
        }

        public override void Act()
        {
            interactor.Interact(transform.parent.gameObject, Sensors.ClosestFoodPositionInSensoryRange(), 0);
        }
    }
}