using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs : MonoBehaviour
{
    [SerializeField] private List<string> needNames;
    [SerializeField] private List<float> needValues;

    public Dictionary<string, float> NeedsDictionary { get; private set; } = new Dictionary<string, float>();

    private void Start()
    {
        UpdateDictionary();
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        UpdateDictionary();     //todo change, because can't change needs in game (because it overrides it with gui value)
#endif
    }

    private void UpdateDictionary()
    {
        for (var i = 0; i < needNames.Count; i++)
        {
            NeedsDictionary[needNames[i]] = needValues[i];
        }
    }
}
