using System.Threading;
using DefaultNamespace;
using Input;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForFoodState : LookingForState
    {
        public override void Start()
        {
            base.Start();
            NextState = transform.parent.gameObject.GetComponentInChildren<HeadingForFoodState>();
        }
        
        protected override bool CanSwitchToNextState()
        {
            try
            {
                Sensors.ClosestFoodPositionInSensoryRange();
                return true;
            }
            catch (TargetNotFoundException)
            {
                return false;
            }
        }
        
        public override float CurrentRank => Needs["Hunger"];
    }
}