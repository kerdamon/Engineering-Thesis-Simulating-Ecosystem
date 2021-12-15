using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EatingCarrotTrainingArea : MonoBehaviour, ITrainingArea
{
    public List<GameObject> Agents { get; set; }
    public List<GameObject> FoodGenerators { get; set; }
    public float range;

    [SerializeField] Transform waterContainterTransform;
    [SerializeField] Transform agentsContainterTransform;
    [SerializeField] Transform foodGeneratorContainterTransform;
    [SerializeField] private int maxRepositionsOnCollisions;


    private void Awake()
    {
        Agents = new List<GameObject>();
        FoodGenerators = new List<GameObject>();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
        {
            ResetArea();
        }
    }

    public void ResetArea()
    {
        StopCoroutine("InnerReset");
        StartCoroutine("InnerReset");
    }

    private IEnumerator InnerReset()
    {
        //ClearFood();
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
        foreach (Transform foodGenerator in foodGeneratorContainterTransform)
        {
            ClearFood(foodGenerator);
            RandomizePositionAndRotationWithCollisionCheck(foodGenerator, foodGeneratorContainterTransform);
        }
        yield return 0;
    }

    void RandomizePositionAndRotationWithCollisionCheck(Transform obj, Transform containterTransform)
    {
        var iterator = 0;
        var newPosition = obj.position;
        var newRotation = obj.rotation;
        while (iterator < maxRepositionsOnCollisions)
        {
            Debug.Log($"iterator: {iterator}, obj: {obj}");
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

    void RandomizePositionAndRotation(Transform gameObject)
    {
        gameObject.localPosition = new Vector3(Random.Range(-range, range), gameObject.localPosition.y, Random.Range(-range, range));
        gameObject.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
    }

    void ClearFood(Transform foodGeneratorTransform)
    {
        foreach(Transform foodObject in foodGeneratorTransform)
        {
            Destroy(foodObject.gameObject);
        }
    }
}
