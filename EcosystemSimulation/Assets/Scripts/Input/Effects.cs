using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private List<string> effectNames;
    [SerializeField] private List<float> effectValues;

    public Dictionary<string, float> EffectsDictionary { get; private set; } = new Dictionary<string, float>();

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
        for (var i = 0; i < effectNames.Count; i++)
        {
            EffectsDictionary[effectNames[i]] = effectValues[i];
        }
    }
}
