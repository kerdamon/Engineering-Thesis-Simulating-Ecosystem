using Interaction;
using Unity.MLAgents;
using UnityEngine;

namespace Interaction.InteractionManagers
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;
        private DrinkingInteraction _drinkingInteraction;
        protected Needs Needs;
        
        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        
        private float agent_bump_into_wall;
        private float agent_interact_with_water;

        protected virtual void Start()
        {
            var parent = transform.parent;
            MovementAgent = parent.GetComponent<MovementAgent>();
            
            Needs = parent.GetComponent<Needs>();
            
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
            interaction.AfterSuccessfulInteraction += () =>
            {
                MovementAgent.AddReward(rewardValue);
                Debug.Log($"Added reward of value {rewardValue} after successful interaction {interaction.GetType()}");
            };
        }

        protected void RegisterUpdatingCurrentInteractionAfterEndOf(Interaction interaction)
        {
            void ClearCurrentInteraction() => CurrentInteraction = null;
            interaction.AfterSuccessfulInteraction += ClearCurrentInteraction;
            interaction.AfterInterruptedInteraction += ClearCurrentInteraction;
        }

        public void InteractIfAbleWith(Interaction interaction, GameObject target)
        {
            if (MovementAgent.WantInteraction && !IsInteracting)
            {
                LaunchNewInteraction(interaction, target);
            }
        }
        
        protected virtual void StartRelevantInteraction(GameObject target)
        {
            switch (target.tag)
            {
                case "Water":
                    if (Needs["Thirst"] > 0)
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
            if (other.gameObject.CompareTag("Wall"))
            {
                Debug.Log($"Added reward of value {agent_bump_into_wall} after bumping into wall");
                MovementAgent.AddReward(agent_bump_into_wall); 
            }
        }
    }
}