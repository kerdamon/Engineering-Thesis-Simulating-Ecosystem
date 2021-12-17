using System;
using UnityEngine;
using Unity.MLAgents;

namespace Interactions
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;

        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        private float agent_bump_into_wall;
        private float agent_interact_with_water;

        protected virtual void Start()
        {
            MovementAgent = transform.parent.GetComponent<MovementAgent>();
            agent_bump_into_wall = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_bump_into_wall", 0.0f);
            agent_interact_with_water = Academy.Instance.EnvironmentParameters.GetWithDefault("agent_interact_with_water", 0.0f);
        }

        public virtual void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Water":
                    MovementAgent.AddReward(agent_interact_with_water);
                    break;
                case "Wall":
                    MovementAgent.AddReward(agent_bump_into_wall);
                    break;
                default:
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