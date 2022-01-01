using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    [SerializeField] private Transform foxesContainer;
    [SerializeField] private Transform rabbitContainer;

    [SerializeField] private Text foxesPopulationText;
    [SerializeField] private Text rabbitsPopulationText;
    [SerializeField] private Text rabbitSpeedMedianText;
    [SerializeField] private Text rabbitSensoryRangeMedianText;
    [SerializeField] private Text rabbitFertilityMedianText;
    [SerializeField] private Text foxSpeedMedianText;
    [SerializeField] private Text foxSensoryRangeMedianText;
    [SerializeField] private Text foxFertilityMedianText;

    [Range(1, 20)]
    [SerializeField] private int updatePeriod = 1;
    
    private void Update()
    {
        if (ShouldUpdateUI()) return;
        UpdateStatsText(foxesPopulationText, $"Foxes on scene: {CountRabbits().ToString()}");
        UpdateStatsText(rabbitsPopulationText, $"Rabbits on scene: {CountFoxes().ToString()}");
        UpdateStatsText(rabbitSpeedMedianText, $"Median of rabbit's speed: {getMedianOfFeatue("Speed", rabbitContainer).ToString(CultureInfo.InvariantCulture)}");
        UpdateStatsText(rabbitSensoryRangeMedianText, $"Median of rabbit's speed: {getMedianOfFeatue("SensoryRange", rabbitContainer).ToString(CultureInfo.InvariantCulture)}");
        UpdateStatsText(rabbitFertilityMedianText, $"Median of rabbit's speed: {getMedianOfFeatue("Fertility", rabbitContainer).ToString(CultureInfo.InvariantCulture)}");
        UpdateStatsText(foxSpeedMedianText, $"Median of rabbit's speed: {getMedianOfFeatue("Speed", foxesContainer).ToString(CultureInfo.InvariantCulture)}");
        UpdateStatsText(foxSensoryRangeMedianText, $"Median of rabbit's speed: {getMedianOfFeatue("SensoryRange", foxesContainer).ToString(CultureInfo.InvariantCulture)}");
        UpdateStatsText(foxFertilityMedianText, $"Median of rabbit's speed: {getMedianOfFeatue("Fertility", foxesContainer).ToString(CultureInfo.InvariantCulture)}");
    }

    private bool ShouldUpdateUI()
    {
        return Time.frameCount % updatePeriod != 0;
    }

    private void UpdateStatsText(Text textElement, string text)
    {
        textElement.text = text;
    }

    private int CountFoxes()
    {
        return CountAgents(foxesContainer);
    }

    private int CountRabbits()
    {
        return CountAgents(rabbitContainer);
    }
    
    private static int CountAgents(Transform container)
    {
        return container.childCount;
    }

    private float getMedianOfFeatue(string featureName, Transform agentsContainer)
    {
        var features = new List<int>();
        foreach (Transform agent in agentsContainer)
        {
            features.Add(agent.GetComponent<Features>()[featureName]);
        }
        return GetMedianFromList(features);
    }

    private float GetMedianFromList(List<int> list)
    {
        var count = list.Count;
        var halfIndex = count / 2;

        var sortedList = list.OrderBy(n => n);
        float median;
        if (count % 2 == 0)
        {
            return (sortedList.ElementAt(halfIndex) + sortedList.ElementAt(halfIndex - 1)) / 2;
        }
        else
        {
            return sortedList.ElementAt(halfIndex);
        }
    }
}
