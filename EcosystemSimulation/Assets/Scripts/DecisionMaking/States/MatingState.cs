using UnityEngine;

namespace DecisionMaking.States
{
    public class MatingState : InteractionState
    {
        public override void Start()
        {
            var parent = transform.parent;
            PreviousState = parent.gameObject.GetComponentInChildren<HeadingForMateState>();
            NextState = parent.gameObject.GetComponentInChildren<ChillingState>(); 
            Interactor = parent.GetComponentInChildren<MatingInteractor>();
            base.Start();
        }

        public override void Act()
        {
            Interactor.Interact(Sensors.ClosestPartnerPositionInSensoryRange());
        }
    }
}