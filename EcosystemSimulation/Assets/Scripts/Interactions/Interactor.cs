using System;
using System.Collections;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactor : MonoBehaviour
    {
        private IEnumerator InteractCoroutine;
        public void Interact(GameObject actor1, GameObject actor2, float time)
        {
            InteractCoroutine = InteractWithWaiting(actor1, actor2, time);
            StartCoroutine(InteractCoroutine);
        }
        
        public Action AfterInteraction;

        private IEnumerator InteractWithWaiting(GameObject actor1, GameObject actor2, float time)
        {
            AtInteractionStart(actor1, actor2);

            var timeElapsed = 0.0f;
            var timeIncrement = 0.1f;
            while (timeElapsed < time)
            {
                timeElapsed += timeIncrement;
                AtInteractionIncrement(timeElapsed / time);
                yield return new WaitForSeconds(timeIncrement);
            }
            
            AtInteractionEnd(actor1, actor2);
            AfterInteraction();
        }

        protected abstract void AtInteractionStart(GameObject actor1, GameObject actor2);
        protected abstract void AtInteractionEnd(GameObject actor1, GameObject actor2);
        protected abstract void AtInteractionIncrement(float percentageCompleted);
    }
}