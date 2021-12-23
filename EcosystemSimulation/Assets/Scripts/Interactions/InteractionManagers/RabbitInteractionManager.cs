using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Policies;

namespace Interactions
{
    public class RabbitInteractionManager : InteractionManager 
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;
        private DrinkingInteraction _drinkingInteraction;
        private MatingInteraction _matingInteraction;
        private TrainingArea _trainingArea; //TODO change; only to use method to randomize position of this agent upon eating in training mode, it is probably spaghetti code

        private MovementAgent _movementAgent;
        private float rabbit_on_eaten;

        private bool is_training;    //TODO check; indicates if agent is currently trained or not (it is used for reseting agent in training scenarios on eaten)

        protected override void Start()
        {
            _trainingArea = GetComponentInParent<TrainingArea>();

            is_training = Mathf.Abs(Academy.Instance.EnvironmentParameters.GetWithDefault("is_training", 0.0f)) > 0.0001f;
            //Debug.Log($"isTraining = {is_training}");

            var rabbit_eating_carrot_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_eating_carrot_reward", 0.0f);
            rabbit_on_eaten = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_on_eaten", 0.0f);

            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
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
                default:
                    break;
            }
            base.Interact(target);
        }

        public float OnEaten()
        {
            _movementAgent.AddReward(rabbit_on_eaten);
            if (is_training)
            {
                _trainingArea.RandomizePositionAndRotationWithCollisionCheck(transform.parent, transform.parent.parent);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
            return 50.0f;   //todo make this depenndent on feature like size
        }
    }
}