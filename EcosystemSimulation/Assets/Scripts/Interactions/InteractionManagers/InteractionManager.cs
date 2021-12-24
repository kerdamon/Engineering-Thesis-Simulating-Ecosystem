using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;
        private DrinkingInteraction _drinkingInteraction;
        protected MatingInteraction MatingInteraction;
        private Needs _needs;
        
        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        
        private float agent_bump_into_wall;
        private float agent_interact_with_water;

        protected virtual void Start()
        {
            var parent = transform.parent;
            MovementAgent = parent.GetComponent<MovementAgent>();
            _drinkingInteraction = GetComponent<DrinkingInteraction>();
            _needs = parent.GetComponent<Needs>();
            //MatingInteraction = GetComponent<MatingInteraction>();
            
            agent_bump_into_wall = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_wall", 0.0f);
            agent_interact_with_water = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_interact_with_water", 0.0f);
            
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
                        CurrentInteraction = _drinkingInteraction;
                        CurrentInteraction.StartInteraction(target);
                    }
                    break;
                case "Wall":
                    MovementAgent.AddReward(agent_bump_into_wall);
                    break;
            }
        }

        public void StopInteraction()
        {
            if(IsInteracting)
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