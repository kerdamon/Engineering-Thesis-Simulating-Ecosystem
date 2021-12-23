using System;
using Unity.MLAgents;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeactivatableDecisionRequester : DecisionRequester
    {
        private Func<DecisionRequestContext, bool> _shouldRequestActionDelegate;
        private Func<DecisionRequestContext, bool> _shouldRequestDecisionDelegate;
        private void OnEnable()
        {
            _shouldRequestActionDelegate = base.ShouldRequestAction;
            _shouldRequestDecisionDelegate = base.ShouldRequestDecision;
        }

        private void OnDisable()
        {
            _shouldRequestActionDelegate = context => false;
            _shouldRequestDecisionDelegate = context => false;
        }

        protected override bool ShouldRequestAction(DecisionRequestContext context)
        {
            return _shouldRequestActionDelegate(context);
            //return base.ShouldRequestAction(context);
        }

        protected override bool ShouldRequestDecision(DecisionRequestContext context)
        {
            return _shouldRequestDecisionDelegate(context); 
            //return base.ShouldRequestDecision(context);
        }
    }
}