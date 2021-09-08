using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class HeadingForFoodState : HeadingForState
    {
        
        public override void Start()
        {
            base.Start();
            var parent = transform.parent;
            NextState = parent.gameObject.GetComponentInChildren<EatingState>();
            PreviousState = parent.gameObject.GetComponentInChildren<LookingForFoodState>(); 
        }

        protected override GameObject FindTarget()
        {
            return Sensors.ClosestFoodPositionInSensoryRange();
        }

        protected override bool CanSwitchToNextState()
        {
            return Actions.ActorsAreInInteractionRange(gameObject, Sensors.ClosestFoodPositionInSensoryRange());
        }
    }
}