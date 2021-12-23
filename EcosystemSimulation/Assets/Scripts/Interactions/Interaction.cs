using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Interactions
{
    public abstract class Interaction : MonoBehaviour
    {
        private IEnumerator _interactCoroutine;
        protected float TimeElapsed;
        [SerializeField] protected float timeIncrement;
        [SerializeField] private float interactionDuration;

        protected GameObject SimulationObject;
        public GameObject SecondSimulationObject { get; set; }
        
        protected virtual void Start()
        {
            SimulationObject = transform.parent.gameObject;
            _interactCoroutine = InteractionCoroutine();
        }

        public void StartInteraction(GameObject secondActor)
        {
            _interactCoroutine = InteractionCoroutine();
            SecondSimulationObject = secondActor;
            StartCoroutine(_interactCoroutine);
        }

        public void Stop()
        {
           StopAllCoroutines();
        }
        
        public Action AfterInteraction;

        private IEnumerator InteractionCoroutine()
        {
            TimeElapsed = 0.0f;
            //AtInteractionStart();
            
            while (TimeElapsed < interactionDuration)
            {
                TimeElapsed += timeIncrement;
                AtInteractionIncrement();
                yield return new WaitForSeconds(timeIncrement);
            }
            
            AtInteractionEnd();
            AfterInteraction();
        }

        //protected abstract void AtInteractionStart();

        protected abstract void AtInteractionEnd();
        
        protected virtual void AtInteractionIncrement()
        {
        }
    }
}