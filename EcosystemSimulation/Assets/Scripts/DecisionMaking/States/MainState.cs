using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class MainState : State
    {
        [SerializeField] protected AnimationCurve scoreCurve;
        public abstract float CurrentRank { get; }
        //todo abstract reference to state machine here, and obtaining it
    }
}