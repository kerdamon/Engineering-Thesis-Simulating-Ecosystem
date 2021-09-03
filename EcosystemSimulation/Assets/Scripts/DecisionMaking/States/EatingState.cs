using UnityEngine;

namespace DecisionMaking.States
{
    public class EatingState : State
    {
        public override float Priority => 0;
        private Sensors _sensors;
        private EatingInteractor _eatingInteractor;

        public void Start()
        {
            _sensors = GetComponentInParent<Sensors>();
            _eatingInteractor = GetComponentInParent<EatingInteractor>();
        }

        public override void Act()
        {
            _eatingInteractor.Interact(gameObject, _sensors.ClosestFoodPositionInSensorsRange(), 0);
        }

        public override float CurrentRank => Priority;
    }
}