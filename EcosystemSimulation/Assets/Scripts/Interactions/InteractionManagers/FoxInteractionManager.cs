using System;
using UnityEngine;
using Unity.MLAgents;

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
            var fox_eating_rabbit_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_eating_rabbit_reward", 0.0f);
            if (Mathf.Abs(fox_eating_rabbit_reward) > 0.0001f)
            {
                _eatingRabbitInteraction.AfterInteraction = () => _movementAgent.AddReward(fox_eating_rabbit_reward);
            }
            _eatingRabbitInteraction.AfterInteraction += () => CurrentInteraction = null;
            
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
                default:
                    break;
            }
            base.Interact(target);
        }
    }
}