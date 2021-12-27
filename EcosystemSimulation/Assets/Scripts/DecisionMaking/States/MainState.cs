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
            Needs = GetComponentInParent<Needs>();
            InteractionManager = transform.parent.GetComponentInChildren<InteractionManager>();
            base.Start();
        }
    }
}