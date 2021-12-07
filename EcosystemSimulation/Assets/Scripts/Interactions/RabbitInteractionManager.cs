using System;
using UnityEngine;

namespace Interactions
{
    public class RabbitInteractionManager : MonoBehaviour
    {
        private EatingInteraction _eatingInteraction;
        public bool IsInteracting => !(_eatingInteraction is null);

        private void Start()
        {
            _eatingInteraction = GetComponent<EatingInteraction>();
        }

        public void Interact(GameObject target)
        {
            switch (target.tag)
            {
                case "Food":
                    //currentInteraction = new interaction()
                    //currentInteraction.Start()
                    //_eatingInteraction.AfterInteraction += _eatingInteraction;
                    
                    _eatingInteraction.AfterInteraction = () => target.GetComponent<MovementAgent>().AddReward(1.0f);
                    _eatingInteraction.Interact(target);
                    break;
                case "Water":
                    //_drinkingInteraction.AfterInteractdion = () => target.GetComponent<MovementAgent>().AddReward(1.0f);
                    break;
            }
        }

        public void StopInteraction()
        {
            //currentInteraction.Stop();
        }
    }
}