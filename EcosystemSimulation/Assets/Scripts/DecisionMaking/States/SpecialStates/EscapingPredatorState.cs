using System;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class EscapingPredatorState : SpecialState
    {
        
        private void OnTriggerStay(Collider other)
        {
            if (!other.isTrigger && other.gameObject.CompareTag("Fox")) //todo change magic number of this agent's predator tag)
            {
                active = true;
                Debug.Log($"Sees predator");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.isTrigger && other.gameObject.CompareTag("Fox"))
            {
                active = false;
                Debug.Log($"Stop seeing predator");                
            }
        }
    }
}