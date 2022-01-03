using Interaction;
using Interaction.RabbitInteractions;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public class RabbitInteractionManager : InteractionManager 
    {
        private EatingCarrotInteraction _eatingCarrotInteraction;

        protected override void Start()
        {
            var rabbit_eating_carrot_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_eating_carrot_reward", 0.0f);
            _eatingCarrotInteraction = GetComponent<EatingCarrotInteraction>();
            AddRewardAfterInteraction(_eatingCarrotInteraction, rabbit_eating_carrot_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_eatingCarrotInteraction);

            base.Start();
        }

        public float OnEaten()
        {
            var parent = transform.parent;
            Destroy(parent.gameObject);
            return 50.0f;   //todo make this depenndent on feature like size
        }
    }
}