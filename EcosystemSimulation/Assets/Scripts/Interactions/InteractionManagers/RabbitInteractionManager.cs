using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Interactions
{
    public class RabbitInteractionManager : InteractionManager 
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;
        private DrinkingInteraction _drinkingInteraction;
        private MatingInteraction _matingInteraction;

        private MovementAgent _movementAgent;

        protected override void Start()
        {
            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
            _eatingCarrotInteraction.AfterInteraction = () => _movementAgent.AddReward(1.0f);
            _eatingCarrotInteraction.AfterInteraction += () => CurrentInteraction = null;
            
            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            _matingInteraction = GetComponent<MatingInteraction>();
            _movementAgent = transform.parent.GetComponent<MovementAgent>();
            base.Start();
        }

        public override void Interact(GameObject target)
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

        public float OnEaten()
        {
            Destroy(transform.parent.gameObject);
            return 50.0f;   //todo make this depenndent on feature like size
        }
    }
}