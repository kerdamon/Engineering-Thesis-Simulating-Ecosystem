using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class HeadingForFoodState : IState
    {
        public float Priority { get; set; } = 0;
        private readonly Needs _needs;
        private ActorActions _actions;
        private Sensors _sensors;
        
        public HeadingForFoodState(GameObject actor)
        {
            _needs = actor.GetComponent<Needs>();
            _actions = actor.GetComponent<ActorActions>();
            _sensors = actor.GetComponent<Sensors>();
        }

        public void Act()
        {
            try
            {
                _actions.MoveToPointUpToDistance(_sensors.ClosestFoodPositionInSensorsRange().transform.position);
            }
            catch (TargetNotFoundException)
            { }
        }

        public float CurrentRank => Priority;
    }
}