using UnityEngine;

namespace DecisionMaking.States
{
    public interface IState
    {
        void Act();
        public float CurrentRank { get; }
        public float Priority { get; set; }
    }
}