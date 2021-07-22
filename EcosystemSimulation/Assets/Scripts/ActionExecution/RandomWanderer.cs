using UnityEngine;

namespace DefaultNamespace
{
    public class RandomWanderer
    {
        private Vector3 _wanderingDirection = Vector3.zero;
        private int _counter;
        public int MaxCounter { get; set; }

        public RandomWanderer(int maxCounter)
        {
            MaxCounter = maxCounter;
        }
        
        public Vector3 GetWanderingDirection()
        {
            if (_counter > MaxCounter)
            {
                _wanderingDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                _counter = 0;
            }
            else
            {
                _counter++;
            }

            return _wanderingDirection;
        }
    }
}