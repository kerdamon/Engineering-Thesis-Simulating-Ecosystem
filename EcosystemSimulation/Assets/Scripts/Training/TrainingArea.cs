using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrainingArea
{
    public List<GameObject> Agents { get; set; }
    public List<GameObject> FoodGenerators { get; set; }

    public void ResetArea();
}
