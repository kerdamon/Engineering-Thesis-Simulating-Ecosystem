using Interaction.InteractionManagers;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class MainState : State
    {
        [SerializeField] protected AnimationCurve scoreCurve;
        protected Needs Needs;
        protected InteractionManager InteractionManager;

        protected override void Start()
        {
            var parent = transform.parent.parent;
            Needs = parent.GetComponent<Needs>();
            InteractionManager = parent.GetComponentInChildren<InteractionManager>();
            base.Start();
        }
    }
}