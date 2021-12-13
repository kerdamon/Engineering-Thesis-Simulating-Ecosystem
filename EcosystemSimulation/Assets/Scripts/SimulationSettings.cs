using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using UnityEngine;

public class SimulationSettings : MonoBehaviour
{
    private List<ITrainingArea> TrainingAreas;
    void Awake()
    {
        Academy.Instance.OnEnvironmentReset += EnvironmentReset;
        TrainingAreas = new List<ITrainingArea>();
    }

    private void EnvironmentReset()
    {
        TrainingAreas = FindObjectsOfType<MonoBehaviour>().OfType<ITrainingArea>().ToList();
        foreach(var trainingArea in TrainingAreas)
        {
            trainingArea.ResetArea();
        }
    }
}
