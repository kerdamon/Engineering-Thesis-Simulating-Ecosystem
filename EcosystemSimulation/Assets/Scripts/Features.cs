using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class Features : DictionarySerializer<int>
{
    [SerializeField] private float maxFeatureValue;
    /// <summary>
    /// This is genetic maintenance cost. It has higher values if agent has better features.
    /// </summary>
    [ShowNativeProperty] public float CurrentGeneticCost => this.Sum(feature => feature.Value);
    [ShowNativeProperty] public float MaxGeneticCost => this.Count() * maxFeatureValue;
    
}
