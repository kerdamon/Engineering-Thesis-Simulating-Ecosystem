using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


namespace DefaultNamespace
{
    [RequireComponent(typeof(Features))]
    public class ActorActions : MonoBehaviour
    {
        private Features _features;

        [SerializeField] private int randomWanderingThreshold;
        [SerializeField] private int interactionRange;
        public RandomWanderer RandomWanderer { get; set; }

        private void Start()
        {
            _features = GetComponent<Features>();
            RandomWanderer = new RandomWanderer(randomWanderingThreshold);
        }

        private void Update()
        {
#if UNITY_EDITOR
            RandomWanderer.MaxCounter = randomWanderingThreshold;
#endif
        }

        public bool ActorsAreInInteractionRange(GameObject actor1, GameObject actor2)
        {
            return (actor1.transform.position - actor2.transform.position).magnitude < interactionRange;
        }

        public void MoveToPointUpToDistance(Vector3 destination)
        {
            var diffX = destination.x - transform.position.x;
            var diffZ = destination.z - transform.position.z;
            var moveDir = new Vector3(diffX, 0f, diffZ);
            transform.position += moveDir.normalized * (_features["Speed"] * Time.deltaTime);
        }

        public void MoveInDirection(Vector3 direction)
        {
            transform.position += direction.normalized * (_features["Speed"] * Time.deltaTime);
        }


    }
}