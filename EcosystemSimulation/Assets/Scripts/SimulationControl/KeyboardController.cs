using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] GameObject simulationMetricsUI;
    private bool issimulationMetricsUIActive;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            issimulationMetricsUIActive = !issimulationMetricsUIActive;
            simulationMetricsUI.SetActive(issimulationMetricsUIActive);
        }
    }
}
