using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrower : MonoBehaviour
{
    [SerializeField] private float size = 0.1f;
    [SerializeField] private float minSize = 0.5f;
    [SerializeField] private float maxSize = 1f;
    [SerializeField] private float growRate = 0.1f;
    [SerializeField] private float growInterval = 5f;

    private void OnEnable()
    {
        size = minSize;
        UpdateScale();
        StartCoroutine(Grow());
    }

    public float OnEaten(float value)
    {
        var eatenValue = value > size ? size : value;
        if ((size - value) <= minSize)
        {
            Destroy(this.gameObject);
        }
        else
        {
            size -= value;
            UpdateScale();
        }
        return eatenValue;
    }

    private IEnumerator Grow()
    {
        while (Math.Abs(size - maxSize) > 0.01f)
        {
            size += growRate;
            UpdateScale();
            yield return new WaitForSeconds(growInterval);
        }
    }

    private void UpdateScale()
    {
        transform.localScale = new Vector3(size, size, size);
    }
}
