using System.Threading;
using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForMateState : LookingForState
    {
        public override void Start()
        {
            base.Start();
            NextState = transform.parent.gameObject.GetComponentInChildren<HeadingForMateState>();
        }
        
        protected override bool CanSwitchToNextState()
        {
            try
            {
                Sensors.ClosestPartnerPositionInSensoryRange();
                return true;
            }
            catch (TargetNotFoundException)
            {
                return false;
            }
        }
        
        public override float CurrentRank => 60;    //todo change magic number
    }
}