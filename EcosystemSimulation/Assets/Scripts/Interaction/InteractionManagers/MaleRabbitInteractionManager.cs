using Interaction.Interactions;
using Interaction.Interactions.RabbitInteractions;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public class MaleRabbitInteractionManager : RabbitInteractionManager 
    {
        private MatingInteraction _matingInteraction;

        protected override void Start()
        {
            var rabbit_mating_reward = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_mating_reward", 0.0f);
            
            _matingInteraction = GetComponent<MatingInteraction>();
            AddRewardAfterInteraction(_matingInteraction, rabbit_mating_reward);
            RegisterUpdatingCurrentInteractionAfterEndOf(_matingInteraction); 
            
            base.Start();
        }

        protected override void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Rabbit-Female":
                    if(Needs.IsMaxOrGreater("ReproductionUrge"))
                        LaunchNewInteraction(_matingInteraction, target);
                    return;
            }
            base.Interact(target);
        }
    }
}