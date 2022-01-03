using Interaction.InteractionManagers;
using UnityEngine;

namespace Interaction.FoxInteractions
{
    public class TrainingEatingRabbitInteraction : EatingRabbitInteraction
    {
        [SerializeField] private TrainingArea trainingArea;

        protected override void AtInteractionEnd()
        {
            var rabbit = SecondSimulationObject.transform;
            var rabbitContainer = rabbit.parent;
            trainingArea.RandomizePositionAndRotationWithCollisionCheck(rabbit, rabbitContainer);
        }
    }
}