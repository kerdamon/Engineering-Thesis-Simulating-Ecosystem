using System;
using System.Collections.Generic;
using UnityEngine;

public class Features : MonoBehaviour
{
    [SerializeField] private List<string> featureNames;
    [SerializeField] private List<float> featureValues;

    public Dictionary<string, float> FeatureDictionary { get; private set; } = new Dictionary<string, float>();

    private void Start()
    {
        UpdateDictionary();
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        UpdateDictionary(); 
#endif
    }

    private void UpdateDictionary()
    {
        for (var i = 0; i < featureNames.Count; i++)
        {
            FeatureDictionary[featureNames[i]] = featureValues[i];
        }
    }

    // void OnGUI()
    // {
    //     foreach (var kvp in _myDictionary)
    //         GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    // } 
}
