using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForFoodState : IState
    {
        public float Priority { get; set; } = 5;
        private readonly Needs _needs;
        private ActorActions _actions;
        
        public LookingForFoodState(GameObject actor)
        {
            _needs = actor.GetComponent<Needs>();
            _actions = actor.GetComponent<ActorActions>();
        }

        public void Act()
        {
            _actions.MoveInDirection(_actions.RandomWanderer.GetWanderingDirection());
        }

        public float CurrentRank => Priority * _needs["Hunger"];
    }
}