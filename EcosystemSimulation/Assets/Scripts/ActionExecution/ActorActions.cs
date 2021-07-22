using System;
using UnityEngine;


namespace DefaultNamespace
{
    [RequireComponent(typeof(Features))]
    public class ActorActions : MonoBehaviour
    {
        private Features _features;

        [SerializeField] private int randomWanderingThreshold;
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

        public void MoveInDirection(Vector3 destination)
        {
            var moveDir = new Vector3(destination.x - transform.position.x, 0f, destination.z - transform.position.z);
            transform.position += moveDir.normalized * (_features.FeatureDictionary["Speed"] * Time.deltaTime);
        }

        public void Interact()
        {
            
        }

        public void WanderRandomly()
        {
            throw new NotImplementedException();
        }
    }
}