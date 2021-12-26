using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentsCounter : MonoBehaviour
{
    [SerializeField] private Text textElement;
    [SerializeField] private GameObject agents;
    void Update()
    {
        var agentsCount = agents.transform.childCount;
        textElement.text = $"Agents on scene: {agentsCount.ToString()}";
    }
}
