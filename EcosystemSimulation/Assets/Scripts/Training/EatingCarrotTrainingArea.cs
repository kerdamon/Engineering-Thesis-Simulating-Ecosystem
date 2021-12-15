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
            foreach (Transform child in agentsContainterTransform)
            {
                RandomizePositionAndRotationWithCollisionCheck(child, agentsContainterTransform);
            }
        }
    }

    public void ResetArea()
    {
        //ClearObjects();
        foreach (Transform child in waterContainterTransform)
        {
            RandomizePositionAndRotation(child);
        }
        foreach (Transform child in agentsContainterTransform)
        {
            RandomizePositionAndRotationWithCollisionCheck(child, agentsContainterTransform);
        }
        foreach (Transform child in foodGeneratorContainterTransform)
        {
            RandomizePositionAndRotation(child);
        }
    }

    void RandomizeObjectPosition(Transform containerTransform)
    {
        foreach(Transform child in containerTransform)
        {
            var collision = containerTransform.GetComponent<BoxCollider>();
            RandomizePositionAndRotation(child);
        }
    }

    void RandomizePositionAndRotationWithCollisionCheck(Transform obj, Transform containterTransform)
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

    void RandomizePositionAndRotation(Transform gameObject)
    {
        gameObject.localPosition = new Vector3(Random.Range(-range, range), gameObject.localPosition.y, Random.Range(-range, range));
        gameObject.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
        gameObject.Translate(Vector3.zero);
        Debug.Log("jest");
    }

    void ClearObjects()
    {
        foreach(var agent in Agents)
        {
            Destroy(agent);
        }
        foreach(var foodObject in FoodGenerators)
        {
            Destroy(foodObject);
        }
        Agents.Clear();
        FoodGenerators.Clear();
    }
}
