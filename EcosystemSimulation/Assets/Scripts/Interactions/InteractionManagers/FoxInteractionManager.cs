using System;
using UnityEngine;

namespace Interactions
{
    public class FoxInteractionManager : InteractionManager
    {
        private EatingRabbitInteraction _eatingRabbitInteraction;
        private DrinkingInteraction _drinkingInteraction;
        private MatingInteraction _matingInteraction;
        
        private MovementAgent _movementAgent;
        
        protected override void Start()
        {
            _eatingRabbitInteraction = GetComponent<EatingRabbitInteraction>();
            _eatingRabbitInteraction.AfterInteraction = () => _movementAgent.AddReward(1.0f);
            _eatingRabbitInteraction.AfterInteraction += () => CurrentInteraction = null;
            
            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            _matingInteraction = GetComponent<MatingInteraction>();
            _movementAgent = transform.parent.GetComponent<MovementAgent>();
            base.Start();
        }
        
        public override void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Rabbit":
                    CurrentInteraction = _eatingRabbitInteraction;
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
    }
}