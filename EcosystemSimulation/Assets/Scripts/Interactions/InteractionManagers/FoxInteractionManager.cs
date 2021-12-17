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
            Debug.Log($"fox_eating_rabbit_reward = {fox_eating_rabbit_reward}");
            if (Mathf.Abs(fox_eating_rabbit_reward) > 0.0001f)
            {
                Debug.Log($"Added reward for fox_eating_rabbit_reward equal to {fox_eating_rabbit_reward}");
                _eatingRabbitInteraction.AfterInteraction = () => _movementAgent.AddReward(fox_eating_rabbit_reward);
            }
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