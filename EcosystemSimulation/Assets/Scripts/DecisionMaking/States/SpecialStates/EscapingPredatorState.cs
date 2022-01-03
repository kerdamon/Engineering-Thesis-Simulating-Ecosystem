using System;
using System.Linq;
using TMPro;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace DecisionMaking.States.EventStates
{
    public class EscapingPredatorState : SpecialState
    {
        private SphereCollider _sphereCollider;

        protected override void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            var colliders = Physics.OverlapSphere(transform.position, _sphereCollider.radius);
            if (colliders.Any(collider1 => (collider1.gameObject.CompareTag("Fox-Male") || collider1.gameObject.CompareTag("Fox-Female")) && !collider1.isTrigger))
            {
                ActivateThis();
                return;
            }
            DeactivateThis();
        }

        // private void OnTriggerStay(Collider other)
        // {
        //     if (!other.gameObject.CompareTag("Fox")) return;
        //     if (!other.isTrigger) //todo change magic number of this agent's predator tag)
        //         ActivateThis();
        //     // else
        //     //     DeactivateThis();
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     if (!other.gameObject.CompareTag("Fox")) return;
        //     if (!other.isTrigger)
        //         DeactivateThis();
        // } 
    }
}