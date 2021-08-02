using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.TextCore;

public class Sensors : MonoBehaviour
{
    private Sensors _sensors;
    private Features _features;

    private void Start()
    {
        _sensors = GetComponent<Sensors>();
        _features = GetComponent<Features>();
    }

    public GameObject ClosestFoodPositionInSensorsRange()
    {
        return ClosestGameObjectWithTagWithinSensoryRange("Food");
    }

    public GameObject ClosestSameSpeciesActorPositionInSensoryRange()
    {
        return ClosestGameObjectWithTagWithinSensoryRange("Rabbit");
    }

    private GameObject ClosestGameObjectWithTagWithinSensoryRange(string tagName)
    {
        var gos = GameObject.FindGameObjectsWithTag(tagName);
        GameObject closest = null;
        var distance = Mathf.Infinity;
        var position = transform.position;
        foreach (var go in gos)
        {
            if (!ObjectIsWithinSensoryRange(go))
                continue;
            if (go == gameObject)
                continue;
            var diff = go.transform.position - position;
            var curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance))
                continue;
            closest = go;
            distance = curDistance;
        }

        if (closest is null)
            throw new TargetNotFoundException();
        return closest;
    }

    private bool ObjectIsWithinSensoryRange(GameObject go)
    {
        return Math.Abs((transform.position - go.transform.position).magnitude) < _features.FeatureDictionary["SensoryRange"];
    }
}

public class TargetNotFoundException : Exception
{
}