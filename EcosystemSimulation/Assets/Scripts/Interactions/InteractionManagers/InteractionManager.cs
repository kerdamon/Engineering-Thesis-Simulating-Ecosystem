using System;
using UnityEngine;

namespace Interactions
{
    public abstract class InteractionManager : MonoBehaviour
    {
        protected MovementAgent MovementAgent;

        public Interaction CurrentInteraction { get; protected set; }
        public bool IsInteracting => !(CurrentInteraction is null);

        protected virtual void Start()
        {
            MovementAgent = transform.parent.GetComponent<MovementAgent>();
        }

        public abstract void Interact(GameObject target);

        public void StopInteraction()
        {
            CurrentInteraction.Stop();
            CurrentInteraction = null;
        }
    }
}