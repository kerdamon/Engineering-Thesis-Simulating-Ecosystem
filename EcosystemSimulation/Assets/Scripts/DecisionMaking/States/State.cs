using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class State : MonoBehaviour
    {
        public abstract void Act();
        public abstract float CurrentRank { get; }
        public abstract float Priority { get; }
        //todo abstract reference to state machine here, and obtaining it
    }
}