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
            Interaction = parent.GetComponentInChildren<MatingInteraction>();
            base.Start();
        }

        public override void Act()
        {
            Interaction.StartInteraction(Sensors.ClosestPartnerPositionInSensoryRange());
        }
    }
}