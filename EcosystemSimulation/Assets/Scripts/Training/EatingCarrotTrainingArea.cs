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

    private void Awake()
    {
        Agents = new List<GameObject>();
        FoodGenerators = new List<GameObject>();
    }

    public void ResetArea()
    {
        ClearObjects();
        RandomizeObjectPosition(waterContainterTransform);
        RandomizeObjectPosition(agentsContainterTransform);
        RandomizeObjectPosition(foodGeneratorContainterTransform);
    }

    void RandomizeObjectPosition(Transform containerTransform)
    {
        foreach(Transform child in containerTransform)
        {
            var collision = containerTransform.GetComponent<BoxCollider>();
            RandomizePositionAndRotation(child);
        }
    }

    void RandomizePositionAndRotation(Transform gameObject)
    {
        gameObject.localPosition = new Vector3(Random.Range(-range, range), gameObject.localPosition.y, Random.Range(-range, range));
        gameObject.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
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
