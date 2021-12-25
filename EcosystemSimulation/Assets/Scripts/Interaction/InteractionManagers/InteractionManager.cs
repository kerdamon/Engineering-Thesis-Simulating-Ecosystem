using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;
        private DrinkingInteraction _drinkingInteraction;
        private Needs _needs;
        
        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        
        private float agent_bump_into_wall;
        private float agent_interact_with_water;

        protected virtual void Start()
        {
            var parent = transform.parent;
            MovementAgent = parent.GetComponent<MovementAgent>();
            
            _needs = parent.GetComponent<Needs>();
            
            agent_bump_into_wall = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_wall", 0.0f);
            agent_interact_with_water = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_interact_with_water", 0.0f);

            MovementAgent.AfterAction += StopInteractionWhenAgentDontWantTo;
            
            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            AddRewardAfterInteraction(_drinkingInteraction, agent_interact_with_water);
            RegisterUpdatingCurrentInteractionAfterEndOf(_drinkingInteraction);
        }

        protected void AddRewardAfterInteraction(Interaction interaction, float rewardValue)
        {
            if (!(Mathf.Abs(rewardValue) > 0.0001f)) return;    //check for 0.0f with epsilon
            interaction.AfterSuccessfulInteraction += () => MovementAgent.AddReward(rewardValue);
        }

        protected void RegisterUpdatingCurrentInteractionAfterEndOf(Interaction interaction)
        {
            void ClearCurrentInteraction() => CurrentInteraction = null;
            interaction.AfterSuccessfulInteraction += ClearCurrentInteraction;
            interaction.AfterInterruptedInteraction += ClearCurrentInteraction;
        }

        protected virtual void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Water":
                    if (_needs["Thirst"] > 0)
                    {
                        LaunchNewInteraction(_drinkingInteraction, target);
                    }
                    return;
                case "Wall":
                    MovementAgent.AddReward(agent_bump_into_wall);
                    return;
            }
        }

        protected void LaunchNewInteraction(Interaction interaction, GameObject target)
        {
            CurrentInteraction = interaction;
            CurrentInteraction.StartInteraction(target);
        }

        private void StopInteractionWhenAgentDontWantTo()
        {
            if(!MovementAgent.WantInteraction && IsInteracting)
                CurrentInteraction.Interrupt();
        }
       
        private void OnTriggerEnter(Collider other)
        {
            if (MovementAgent.WantInteraction && !IsInteracting)
            {
                Debug.Log($"Rozpoczynam interakcje");
                Interact(other.gameObject);
            }
        }
    }
}