using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSCSizeChanger : MonoBehaviour
{
    [SerializeField] private float maxSensorySphereColliderRadius;
    [SerializeField] private float minSensorySphereColliderRadius;

    void Update()
    {
        var collider = GetComponentInChildren<SphereCollider>(); //todo change if there will be other sphere colliders
        var offspringFeatures = transform.parent.GetComponentInParent<Features>();
        collider.radius = CalculateSensorySphereColliderRadius(offspringFeatures);
    }

    private float CalculateSensorySphereColliderRadius(Features features)
    {
        return (maxSensorySphereColliderRadius - minSensorySphereColliderRadius) / 100.0f *
            features["SensoryRange"] + minSensorySphereColliderRadius;
    }
}