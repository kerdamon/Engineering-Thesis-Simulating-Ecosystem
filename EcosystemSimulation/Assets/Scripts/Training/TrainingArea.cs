using System.Collections;
using UnityEngine;

public class TrainingArea : MonoBehaviour, ITrainingArea
{
    public float range;

    [SerializeField] Transform waterContainterTransform;
    [SerializeField] Transform agentsContainterTransform;
    [SerializeField] private int maxRepositionsOnCollisions;

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
        foreach (Transform agent in agentsContainterTransform)
        {
            RandomizePositionAndRotationWithCollisionCheck(agent, agentsContainterTransform);
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
            newPosition = containterTransform.TransformPoint(new Vector3(Random.Range(-range, range), obj.localPosition.y, Random.Range(-range, range)));
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
        gameObject.localPosition = new Vector3(Random.Range(-range, range), gameObject.localPosition.y, Random.Range(-range, range));
        gameObject.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }
}
