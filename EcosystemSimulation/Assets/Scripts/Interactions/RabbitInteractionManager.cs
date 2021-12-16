using System;
using UnityEngine;

namespace Interactions
{
    public class RabbitInteractionManager : MonoBehaviour
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;
        private DrinkingInteraction _drinkingInteraction;
        private MatingInteraction _matingInteraction;

        private MovementAgent _movementAgent;

        public Interaction CurrentInteraction { get; private set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        private void Start()
        {
            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
            _eatingCarrotInteraction.AfterInteraction = () => _movementAgent.AddReward(1.0f);
            _eatingCarrotInteraction.AfterInteraction += () => CurrentInteraction = null;
            
            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            _matingInteraction = GetComponent<MatingInteraction>();
            _movementAgent = transform.parent.GetComponent<MovementAgent>();
        }

        public void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Food":
                    CurrentInteraction = _eatingCarrotInteraction;
                    CurrentInteraction.StartInteraction(target);
                    break;
                case "Water":
                    //_movementAgent.AddReward(-1.0f);
                    break;
                case "Wall":
                    //_movementAgent.AddReward(-1.0f);
                    break;
                default:
                    break;
            }
        }

        public void StopInteraction()
        {
            CurrentInteraction.Stop();
            CurrentInteraction = null;
        }
    }
}