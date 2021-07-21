using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    [RequireComponent(typeof(Features))]
    public class ActorActions : MonoBehaviour
    {
        private Features _features;

        private void Start()
        {
            _features = GetComponent<Features>();
            GetComponent<StateMachine>().moveAction += MoveInDirection;
        }

        public void MoveInDirection(Vector3 destination)
        {
            var position = transform.position;
            var x = position.x;
            var z = position.z;
            transform.position += new Vector3(destination.x - x, 0, destination.z - z) * _features.FeatureDictionary["Speed"];
        }

        public void Interact()
        {
            
        }
    }
}