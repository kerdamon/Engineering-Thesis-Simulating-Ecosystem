using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodGenerator : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float spawningRadius;
    [SerializeField] private int plantLimit;

    private void Start()
    {
        StartCoroutine(GenerateFood());
    }

    private IEnumerator GenerateFood()
    {
        while (true)
        {
            if (!PlantLimitReached())
            {
                var relativePosition = RandomizeRelativePosition();
                SpawnOneFoodInPosition(relativePosition);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 RandomizeRelativePosition()
    {
        var angle = Random.Range(0f, 2 * Mathf.PI);
        var radius = Random.Range(0f, spawningRadius);
        var x = radius * Mathf.Cos(angle);
        var z = radius * Mathf.Sin(angle);
        var relativePosition = new Vector3(x, 0f, z);
        var distance = Random.Range(0f, spawningRadius);
        return relativePosition * distance;
    }

    private bool PlantLimitReached()
    {
        return transform.childCount >= plantLimit;
    }

    private void SpawnOneFoodInPosition(Vector3 position)
    {
        var instantiatedFood = Instantiate(foodPrefab, transform);
        instantiatedFood.transform.Translate(position);
    }
}
