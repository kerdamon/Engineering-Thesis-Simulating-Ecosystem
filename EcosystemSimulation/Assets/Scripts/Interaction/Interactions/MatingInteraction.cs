using UnityEngine;

namespace Interaction.Interactions
{
    public class MatingInteraction : Interaction
    {
        [SerializeField] private int maxChildrenPerLitter;
        
        protected override void AtInteractionEnd()
        {
            SpawnOffspring(SecondSimulationObject);
        }

        private void SpawnOffspring(GameObject mate)
        {
            var numberOfChildren = Random.value * maxChildrenPerLitter;
            for (var i = 0; i < numberOfChildren; i++)
            {
                var offspring = Instantiate(gameObject.transform.parent.gameObject, transform.parent.parent);
                offspring.transform.Translate(Random.value * 2, 0, Random.value * 2);
                var offspringFeatures = offspring.GetComponent<Features>();
                var actorFeatures = SimulationObject.GetComponent<Features>();
                var mateFeatures = mate.GetComponentInParent<Features>();
                
                //crossover
                foreach (var f in actorFeatures)
                {
                    offspringFeatures[f.Key] = Random.value > 0.5f ? f.Value : mateFeatures[f.Key];
                }

                // //mutation
                // foreach (var f in actorFeatures)
                // {
                //     offspringFeatures[f.Key] = Random.value > 0.5f ? f.Value : mateFeatures[f.Key];
                // } 
            }
        }
    }
}
