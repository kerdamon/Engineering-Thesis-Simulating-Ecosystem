using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class ChillingState : State
    {
        public float Priority => 30;

        private ActorActions _actions;
        
        public void Start()
        {
            _actions = GetComponentInParent<ActorActions>(); 
        }
        
        public override void Act()
        {
            _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
        }

        public override float CurrentRank => Priority;
    }
}