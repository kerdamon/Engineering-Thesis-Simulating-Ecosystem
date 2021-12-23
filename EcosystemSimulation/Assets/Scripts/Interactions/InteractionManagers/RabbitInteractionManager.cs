using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public class RabbitInteractionManager : InteractionManager 
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;

        private TrainingArea _trainingArea; //TODO change; only to use method to randomize position of this agent upon eating in training mode, it is probably spaghetti code

        private float rabbit_on_eaten;  //todo change name and add reward at the end

        private bool _isTraining;    //TODO check; indicates if agent is currently trained or not (it is used for reseting agent in training scenarios on eaten)

        protected override void Start()
        {
            _trainingArea = GetComponentInParent<TrainingArea>();
            _isTraining = Mathf.Abs(Academy.Instance.EnvironmentParameters.GetWithDefault("is_training", 0.0f)) > 0.0001f;

            var rabbit_eating_carrot_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_eating_carrot_reward", 0.0f);
            rabbit_on_eaten = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_on_eaten", 0.0f);

            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
            AddRewardAfterInteraction(_eatingCarrotInteraction, rabbit_eating_carrot_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_eatingCarrotInteraction);
            
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
            }
            base.Interact(target);
        }

        public float OnEaten()
        {
            var parent = transform.parent;
            MovementAgent.AddReward(rabbit_on_eaten);
            if (_isTraining)
            {
                _trainingArea.RandomizePositionAndRotationWithCollisionCheck(parent, parent.parent);
            }
            else
            {
                Destroy(parent.gameObject);
            }
            return 50.0f;   //todo make this depenndent on feature like size
        }
    }
}