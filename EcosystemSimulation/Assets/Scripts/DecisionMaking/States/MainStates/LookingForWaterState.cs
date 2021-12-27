using Interaction;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForWaterState : MainState
    {
        public override float CurrentRank => scoreCurve.Evaluate(Needs["Thirst"]);
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Water") && Needs["Thirst"] > 0) //todo abstract this method to State
                InteractionManager.InteractIfAbleWith(DrinkingInteraction, other.gameObject);
        }
    }
}