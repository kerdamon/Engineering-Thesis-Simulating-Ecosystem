using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class MainState : State
    {
        [SerializeField] protected AnimationCurve scoreCurve;
    }
}