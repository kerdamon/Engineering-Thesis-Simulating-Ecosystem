using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class ChillingState : IState
    {
        public float Priority { get; set; } = 100;

        private ActorActions _actions;
        
        public ChillingState(GameObject actor)
        {
            _actions = actor.GetComponent<ActorActions>(); 
        }
        
        public void Act()
        {
            _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
        }

        public float CurrentRank => Priority;
    }
}