using System.Collections;
using System.Security.Cryptography.X509Certificates;
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

        IEnumerator InteractWithWaiting(GameObject actor1, GameObject actor2, float time)
        {
            StartInteraction(actor1, actor2);

            var timeElapsed = 0.0f;
            var timeIncrement = 0.1f;
            while (timeElapsed < time)
            {
                timeElapsed += timeIncrement;
                WaitingIncrement(timeElapsed / time);
                yield return new WaitForSeconds(timeIncrement);
            }
            
            EndInteraction(actor1, actor2);
        }

        protected abstract void StartInteraction(GameObject actor1, GameObject actor2);
        protected abstract void EndInteraction(GameObject actor1, GameObject actor2);
        protected abstract void WaitingIncrement(float percentageCompleted);
    }
}