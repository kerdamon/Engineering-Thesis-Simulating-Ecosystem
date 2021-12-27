using System;
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
            enabled = false;
        }

        public override void OnEnterState()
        {
            Debug.Log($"Enabluje {gameObject.name}");
            enabled = true;
            base.OnEnterState();
        }
        
        public override void OnLeaveState()
        {
            Debug.Log($"Disabluje {gameObject.name}");
            enabled = false;
            base.OnLeaveState();
        }
    }
}