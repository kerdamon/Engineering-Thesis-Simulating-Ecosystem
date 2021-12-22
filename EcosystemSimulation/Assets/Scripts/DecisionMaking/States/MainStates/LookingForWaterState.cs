using System;
using DefaultNamespace;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForWaterState : MainState
    {
        private Needs _needs;

        private void Awake()
        {
            _needs = GetComponentInParent<Needs>();
        }
        
        public override float CurrentRank => scoreCurve.Evaluate(_needs["Thirst"]);

        public override void prepareModel()
        {
            Debug.Log($"Przygotowuje model do stanu {nameof(LookingForWaterState)}");
        }
    }
}