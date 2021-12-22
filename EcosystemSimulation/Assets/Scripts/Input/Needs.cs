using System;
using Input;
using UnityEngine;

public class Needs : DictionarySerializer<float>
{
    [SerializeField] private float increaseRate;
    private void Update()
    {
        this["Hunger"] += Time.deltaTime * increaseRate;
        this["ReproductionUrge"] += Time.deltaTime * increaseRate;
    }
}
