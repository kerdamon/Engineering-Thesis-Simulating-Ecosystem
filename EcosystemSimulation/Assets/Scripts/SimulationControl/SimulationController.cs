using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DefaultNamespace.SimulationControl;
using Unity.Mathematics;
using Unity.MLAgents;
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

    [Range(10, 100)]
    [SerializeField] private int logPeriod = 20;

    private FileLogger _fileLogger;

    private void Awake()
    {
        _fileLogger = GetComponent<FileLogger>();
    }

    private void Start()
    {
        _fileLogger.LogLine("Timestamp,FoxesPopulation,RabbitPopulation,RabbitSpeedMedian,RabbitSensoryRangeMedian,RabbitFertilityMedian,FoxSpeedMedian,FoxSensoryRangeMedian,FoxFertilityMedian");
    }

    private void Update()
    {
        if (!ShouldLogToFile() && !ShouldUpdateUI()) return;
        var foxesPopulation = CountRabbits();
        var rabbitsPopulation = CountFoxes();
        var rabbitSpeedMedian = getMedianOfFeatue("Speed", rabbitContainer);
        var rabbitSensoryRangeMedian = getMedianOfFeatue("SensoryRange", rabbitContainer);
        var rabbitFertilityMedian = getMedianOfFeatue("Fertility", rabbitContainer);
        var foxSpeedMedian = getMedianOfFeatue("Speed", foxesContainer);
        var foxSensoryRangeMedian = getMedianOfFeatue("SensoryRange", foxesContainer);
        var foxFertilityMedian = getMedianOfFeatue("Fertility", foxesContainer);

        if (ShouldUpdateUI())
        {
            UpdateStatsText(foxesPopulationText, $"Foxes on scene: {foxesPopulation.ToString()}");
            UpdateStatsText(rabbitsPopulationText, $"Rabbits on scene: {rabbitsPopulation.ToString()}");
            UpdateStatsText(rabbitSpeedMedianText,
                $"Median of rabbit's speed: {rabbitSpeedMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(rabbitSensoryRangeMedianText,
                $"Median of rabbit's speed: {rabbitSensoryRangeMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(rabbitFertilityMedianText,
                $"Median of rabbit's speed: {rabbitFertilityMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(foxSpeedMedianText,
                $"Median of rabbit's speed: {foxSpeedMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(foxSensoryRangeMedianText,
                $"Median of rabbit's speed: {foxSensoryRangeMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(foxFertilityMedianText,
                $"Median of rabbit's speed: {foxFertilityMedian.ToString(CultureInfo.InvariantCulture)}");
        }

        if (ShouldLogToFile())
        {
            _fileLogger.LogLine($"{Academy.Instance.StepCount.ToString()}" +
                                $"{foxesPopulation.ToString()}," +
                                $"{rabbitsPopulation.ToString()}," +
                                $"{rabbitSpeedMedian.ToString(CultureInfo.InvariantCulture)}," +
                                $"{rabbitSensoryRangeMedian.ToString(CultureInfo.InvariantCulture)}," +
                                $"{rabbitFertilityMedian.ToString(CultureInfo.InvariantCulture)}," +
                                $"{foxSpeedMedian.ToString(CultureInfo.InvariantCulture)}," +
                                $"{foxSensoryRangeMedian.ToString(CultureInfo.InvariantCulture)}," +
                                $"{foxFertilityMedian.ToString(CultureInfo.InvariantCulture)}");
        }
    }

    private bool ShouldLogToFile()
    {
        return Time.frameCount % logPeriod == 0;
    }

    private bool ShouldUpdateUI()
    {
        return Time.frameCount % updatePeriod == 0;
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
