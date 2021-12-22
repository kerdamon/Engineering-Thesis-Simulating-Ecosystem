using System;
using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForFoodState : MainState
    {
        private Needs _needs;

        private void Awake()
        {
            _needs = GetComponentInParent<Needs>();
        }
        
        public override float CurrentRank => scoreCurve.Evaluate(_needs["Hunger"]);

        public override void prepareModel()
        {
            Debug.Log($"Przygotowuje model do stanu {nameof(LookingForFoodState)}");
        }
    }
}