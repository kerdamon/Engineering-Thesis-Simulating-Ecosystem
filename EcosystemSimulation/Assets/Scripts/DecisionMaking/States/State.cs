using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class State : MonoBehaviour
    {
        public virtual void PrepareModel()
        {
            
        }
        public abstract float CurrentRank { get; }
    }
}