using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class HeadingForMateState : HeadingForState
    {
        
        public override void Start()
        {
            base.Start();
            var parent = transform.parent;
            NextState = parent.gameObject.GetComponentInChildren<MatingState>();
            PreviousState = parent.gameObject.GetComponentInChildren<LookingForMateState>(); 
        }

        protected override GameObject FindTarget()
        {
            return Sensors.ClosestPartnerPositionInSensoryRange();
        }

        protected override bool CanSwitchToNextState()
        {
            return Actions.ActorsAreInInteractionRange(gameObject, Sensors.ClosestPartnerPositionInSensoryRange());
        }
    }
}