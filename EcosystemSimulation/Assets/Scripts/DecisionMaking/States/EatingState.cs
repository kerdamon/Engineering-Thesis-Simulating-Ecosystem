using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class EatingState : IState
    {
        public float Priority { get; set; } = 0;
        private Sensors _sensors;
        private EatingInteractor _eatingInteractor;

        private GameObject _actor;

        public EatingState(GameObject actor)
        {
            _actor = actor;
            _sensors = actor.GetComponent<Sensors>();
            _eatingInteractor = actor.GetComponent<EatingInteractor>();
        }

        public void Act()
        {
            _eatingInteractor.Interact(_actor, _sensors.ClosestFoodPositionInSensorsRange(), 0);
        }

        public float CurrentRank => Priority;
    }
}