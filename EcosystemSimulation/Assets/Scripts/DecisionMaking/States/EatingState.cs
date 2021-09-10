using UnityEngine;

namespace DecisionMaking.States
{
    public class EatingState : InteractionState
    {
        public override void Start()
        {
            var parent = transform.parent.gameObject;
            PreviousState = parent.GetComponentInChildren<HeadingForFoodState>();
            NextState = parent.GetComponentInChildren<ChillingState>(); 
            Interactor = parent.GetComponentInChildren<EatingInteractor>();
            base.Start();
        }

        public override void Act()
        {
            Debug.Log("act");
            Interactor.Interact(Sensors.ClosestFoodPositionInSensoryRange());
        }
    }
}