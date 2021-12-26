using Interaction.InteractionManagers;

namespace Interaction.Interactions.FoxInteractions
{
    public class EatingRabbitInteraction : Interaction
    {
        private Needs _needs;
    
        protected override void Start()
        {
            base.Start();
            _needs = SimulationObject.GetComponent<Needs>();
        }
        protected override void AtInteractionEnd()
        {
            var rabbitInteractionManager = SecondSimulationObject.GetComponentInChildren<RabbitInteractionManager>();
            var energyReceived = rabbitInteractionManager.OnEaten();
            _needs["Hunger"] -= energyReceived;
        }
    }
}