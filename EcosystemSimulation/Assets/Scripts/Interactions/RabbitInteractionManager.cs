using System;
using UnityEngine;

namespace Interactions
{
    public class RabbitInteractionManager : MonoBehaviour
    {
        private EatingInteraction _eatingInteraction;
        private DrinkingInteraction _drinkingInteraction;
        private MatingInteraction _matingInteraction;

        private MovementAgent _movementAgent;

        public Interaction CurrentInteraction { get; private set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        private void Start()
        {
            _eatingInteraction = GetComponent<EatingInteraction>();
            _eatingInteraction.AfterInteraction = () => _movementAgent.AddReward(1.0f);
            _eatingInteraction.AfterInteraction += () => CurrentInteraction = null;
            
            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            _matingInteraction = GetComponent<MatingInteraction>();
            _movementAgent = transform.parent.GetComponent<MovementAgent>();
        }

        public void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Food":
                    CurrentInteraction = _eatingInteraction;
                    CurrentInteraction.StartInteraction(target);
                    break;
                case "Water":
                    //_drinkingInteraction.AfterInteractdion = () => target.GetComponent<MovementAgent>().AddReward(1.0f);
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