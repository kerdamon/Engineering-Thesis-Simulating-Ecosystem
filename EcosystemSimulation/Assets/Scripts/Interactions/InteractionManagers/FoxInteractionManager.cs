using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public class FoxInteractionManager : InteractionManager
    {
        private EatingRabbitInteraction _eatingRabbitInteraction;
        
        protected override void Start()
        {
            _eatingRabbitInteraction = GetComponent<EatingRabbitInteraction>();
            var fox_eating_rabbit_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_eating_rabbit_reward", 0.0f);
            // if (Mathf.Abs(fox_eating_rabbit_reward) > 0.0001f)
            // {
            //     _eatingRabbitInteraction.AfterInteraction = () => MovementAgent.AddReward(fox_eating_rabbit_reward);
            // }
            // _eatingRabbitInteraction.AfterInteraction += () => CurrentInteraction = null;
            AddRewardAfterInteraction(_eatingRabbitInteraction, fox_eating_rabbit_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_eatingRabbitInteraction);
            
            base.Start();
        }

        protected override void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Rabbit":
                    CurrentInteraction = _eatingRabbitInteraction;
                    CurrentInteraction.StartInteraction(target);
                    break;
            }
            base.Interact(target);
        }
    }
}