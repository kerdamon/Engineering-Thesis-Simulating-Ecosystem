using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class TrainingArea : MonoBehaviour, ITrainingArea
{
    [SerializeField] Transform waterContainterTransform;
    [SerializeField] private int maxRepositionsOnCollisions;

    [SerializeField] private Transform geographicalObjectsContainer;
    [ShowNativeProperty] public float ContentSetupRange => geographicalObjectsContainer.localScale.x * 100;

    public void ResetArea()
    {
        StopCoroutine(nameof(InnerReset));
        StartCoroutine(nameof(InnerReset));
    }

    protected virtual IEnumerator InnerReset()
    {
        foreach (Transform water in waterContainterTransform)
        {
            RandomizePositionAndRotation(water);
        }
        yield return 0;
    }

    public void RandomizePositionAndRotationWithCollisionCheck(Transform obj, Transform containterTransform)
    {
        var iterator = 0;
        var newPosition = obj.position;
        var newRotation = obj.rotation;
        while (iterator < maxRepositionsOnCollisions)
        {
            newPosition = containterTransform.TransformPoint(new Vector3(Random.Range(-ContentSetupRange, ContentSetupRange), obj.localPosition.y, Random.Range(-ContentSetupRange, ContentSetupRange)));
            newRotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            var isCollision = Physics.CheckBox(newPosition, obj.localScale / 2);
            if(!isCollision)
            {
                break;
            }
            iterator++;
        }
        obj.position = newPosition;
        obj.rotation = newRotation;
    }

    protected void RandomizePositionAndRotation(Transform gameObject)
    {
        gameObject.localPosition = new Vector3(Random.Range(-ContentSetupRange, ContentSetupRange), gameObject.localPosition.y, Random.Range(-ContentSetupRange, ContentSetupRange));
        gameObject.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }
}
