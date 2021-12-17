using System;
using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public class RabbitInteractionManager : InteractionManager 
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;
        private DrinkingInteraction _drinkingInteraction;
        private MatingInteraction _matingInteraction;

        private MovementAgent _movementAgent;
        private float rabbit_on_eaten;

        protected override void Start()
        {
            var rabbit_eating_carrot_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_eating_carrot_reward", 0.0f);
            rabbit_on_eaten = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_on_eaten", 0.0f);

            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
            Debug.Log($"rabbit_eating_carrot_reward = {rabbit_eating_carrot_reward}");
            if (Mathf.Abs(rabbit_eating_carrot_reward) > 0.0001f)
            {
                Debug.Log($"Added reward for rabbit_eating_carrot_reward equal to {rabbit_eating_carrot_reward}");
                _eatingCarrotInteraction.AfterInteraction = () => _movementAgent.AddReward(rabbit_eating_carrot_reward);
            }
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
            _movementAgent.AddReward(rabbit_on_eaten);
            Debug.Log($"Eaten rabbit {gameObject.name} and added reward for it equal to {rabbit_on_eaten}");
            Destroy(transform.parent.gameObject);
            return 50.0f;   //todo make this depenndent on feature like size
        }
    }
}