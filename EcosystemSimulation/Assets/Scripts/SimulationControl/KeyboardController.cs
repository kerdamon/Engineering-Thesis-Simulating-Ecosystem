using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] GameObject simulationMetricsUIStats;
    [SerializeField] GameObject simulationMetricsUIDeath;
    private bool issimulationMetricsUIStatsActive;
    private bool issimulationMetricsUIDeathActive;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(issimulationMetricsUIDeathActive)
                simulationMetricsUIDeath.SetActive(false); 
            issimulationMetricsUIStatsActive = !issimulationMetricsUIStatsActive;
            simulationMetricsUIStats.SetActive(issimulationMetricsUIStatsActive);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(issimulationMetricsUIStatsActive)
                simulationMetricsUIStats.SetActive(false);  
            issimulationMetricsUIDeathActive = !issimulationMetricsUIDeathActive;
            simulationMetricsUIDeath.SetActive(issimulationMetricsUIDeathActive);
        }
    }
}
