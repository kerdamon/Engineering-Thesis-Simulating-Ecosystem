﻿using System;
using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;
        protected DrinkingInteraction DrinkingInteraction;
        protected MatingInteraction MatingInteraction;

        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        private float agent_bump_into_wall;
        private float agent_interact_with_water;

        protected virtual void Start()
        {
            var parent = transform.parent;
            MovementAgent = parent.GetComponent<MovementAgent>();
            DrinkingInteraction = GetComponent<DrinkingInteraction>();
            //MatingInteraction = GetComponent<MatingInteraction>();
            
            agent_bump_into_wall = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_wall", 0.0f);
            agent_interact_with_water = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_interact_with_water", 0.0f);
            
            AddRewardAfterInteraction(DrinkingInteraction, agent_interact_with_water);
            RegisterUpdatingCurrentInteractionAfterEndOf(DrinkingInteraction);
        }

        protected void AddRewardAfterInteraction(Interaction interaction, float rewardValue)
        {
            if (!(Mathf.Abs(rewardValue) > 0.0001f)) return;    //check for 0.0f with epsilon
            //Debug.Log($"Added reward for {interaction.name} equal to {rewardValue}");
            interaction.AfterInteraction = () => MovementAgent.AddReward(rewardValue);
        }

        protected void RegisterUpdatingCurrentInteractionAfterEndOf(Interaction interaction)
        {
            interaction.AfterInteraction += () => CurrentInteraction = null; 
        }

        public virtual void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Water":
                    CurrentInteraction = DrinkingInteraction;
                    CurrentInteraction.StartInteraction(target);
                    break;
                case "Wall":
                    MovementAgent.AddReward(agent_bump_into_wall);
                    break;
            }
        }

        public void StopInteraction()
        {
            CurrentInteraction.Stop();
            CurrentInteraction = null;
        }
    }
}