using System.Collections;
using UnityEngine;

namespace Training
{
    public class PredatorTrainingArea : TrainingArea
    {
        [SerializeField] private Transform predatorsContainerTransform;

        protected override IEnumerator InnerReset()
        {
            yield return base.InnerReset();
            foreach (Transform agent in predatorsContainerTransform)
            {
                RandomizePositionAndRotationWithCollisionCheck(agent, predatorsContainerTransform);
            }
            yield return 0;
        }
    }
}