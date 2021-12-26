using UnityEngine;

namespace Interaction.Interactions.RabbitInteractions
{
    public class EatingCarrotInteraction : Interaction
    {
        [SerializeField] private float energyReceivedPerBite;
        [SerializeField] private float biteSize;

        private Needs _needs;
    
        protected override void Start()
        {
            base.Start();
            _needs = SimulationObject.GetComponent<Needs>();
        }
        protected override void AtInteractionEnd()
        {
            var eatenValue = SecondSimulationObject.GetComponent<PlantGrower>().OnEaten(biteSize);
            _needs["Hunger"] -= eatenValue * energyReceivedPerBite;
        }
    }
}