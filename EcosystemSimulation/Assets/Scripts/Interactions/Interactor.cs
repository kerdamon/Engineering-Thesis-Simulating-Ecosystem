using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Interactions
{
    public abstract class Interactor : MonoBehaviour
    {
        private IEnumerator InteractCoroutine;
        protected float TimeElapsed;
        [SerializeField] protected float timeIncrement;
        [SerializeField] private float interactionDuration;

        protected GameObject Actor;
        public GameObject SecondActor { get; set; }
        
        protected virtual void Start()
        {
            Actor = transform.parent.gameObject;
            InteractCoroutine = InteractWithWaiting();
        }

        public void Interact(GameObject secondActor)
        {
            SecondActor = secondActor;
            StartCoroutine(InteractCoroutine);
        }
        
        public Action AfterInteraction;

        private IEnumerator InteractWithWaiting()
        {
            TimeElapsed = 0.0f;
            AtInteractionStart();
            
            while (TimeElapsed < interactionDuration)
            {
                TimeElapsed += timeIncrement;
                AtInteractionIncrement();
                yield return new WaitForSeconds(timeIncrement);
            }
            
            AtInteractionEnd();
            AfterInteraction();
        }

        protected virtual void AtInteractionStart()
        {
        }
        protected virtual void AtInteractionEnd()
        {
            InteractCoroutine = InteractWithWaiting();
        }
        protected virtual void AtInteractionIncrement()
        {
        }
    }
}