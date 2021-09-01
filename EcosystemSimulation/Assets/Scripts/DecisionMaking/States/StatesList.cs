using System.Collections.Generic;
using UnityEngine;

namespace DecisionMaking.States
{
    public class StatesList : MonoBehaviour
    {
        public List<IState> States { get; set; }

        private void Awake()
        {
            var o = gameObject;
            States = new List<IState>
            {
                new ChillingState(o),
                new LookingForFoodState(o)
            };
        }
    }
}