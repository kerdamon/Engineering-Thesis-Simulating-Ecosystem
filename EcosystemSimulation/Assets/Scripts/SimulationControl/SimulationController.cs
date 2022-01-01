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
    
    [SerializeField] private Text minRabbitLifeTimeText;
    [SerializeField] private Text averageRabbitLifeTimeText;
    [SerializeField] private Text maxRabbitLifeTimeText;
    
    [SerializeField] private Text minFoxLifeTimeText;
    [SerializeField] private Text averageFoxLifeTimeText;
    [SerializeField] private Text maxFoxLifeTimeText;

    [Range(1, 20)] [SerializeField] private int updatePeriod = 1;

    [Range(10, 100)] [SerializeField] private int logPeriod = 20;

    private FileLogger _fileLogger;

    private void Awake()
    {
        _fileLogger = GetComponent<FileLogger>();
    }

    private void Start()
    {
        _fileLogger.LogLine(
            "Timestamp,FoxesPopulation,RabbitPopulation,RabbitSpeedMedian,RabbitSensoryRangeMedian,RabbitFertilityMedian,FoxSpeedMedian,FoxSensoryRangeMedian,FoxFertilityMedian,MinRabbitLifeTime,AverageRabbitLifeTime,MaxRabbitLifeTime,MinFoxLifeTime,AverageFoxLifeTime,MaxFoxLifeTime");
    }

    private void Update()
    {
        if (!ShouldLogToFile() && !ShouldUpdateUI()) return;
        var foxesPopulation = CountRabbits();
        var rabbitsPopulation = CountFoxes();
        var rabbitSpeedMedian = GetMedianOfFeature("Speed", rabbitContainer);
        var rabbitSensoryRangeMedian = GetMedianOfFeature("SensoryRange", rabbitContainer);
        var rabbitFertilityMedian = GetMedianOfFeature("Fertility", rabbitContainer);
        var foxSpeedMedian = GetMedianOfFeature("Speed", foxesContainer);
        var foxSensoryRangeMedian = GetMedianOfFeature("SensoryRange", foxesContainer);
        var foxFertilityMedian = GetMedianOfFeature("Fertility", foxesContainer);
        var (minRabbitLifeTime, averageRabbitLifeTime, maxRabbitLifeTime) = GetLifeTimeOfAgents(rabbitContainer);
        var (minFoxLifeTime, averageFoxLifeTime, maxFoxLifeTime) = GetLifeTimeOfAgents(foxesContainer);

        if (ShouldUpdateUI())
        {
            UpdateStatsText(foxesPopulationText, $"Foxes on scene: {foxesPopulation.ToString()}");
            UpdateStatsText(rabbitsPopulationText, $"Rabbits on scene: {rabbitsPopulation.ToString()}");
            
            UpdateStatsText(rabbitSpeedMedianText,
                $"Rabbit Speed Median: {rabbitSpeedMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(rabbitSensoryRangeMedianText,
                $"Rabbit Sensory Range Median: {rabbitSensoryRangeMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(rabbitFertilityMedianText,
                $"Rabbit Fertility Median: {rabbitFertilityMedian.ToString(CultureInfo.InvariantCulture)}");
            
            UpdateStatsText(foxSpeedMedianText,
                $"Fox Speed Median: {foxSpeedMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(foxSensoryRangeMedianText,
                $"Fox Sensory Range Median: {foxSensoryRangeMedian.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(foxFertilityMedianText,
                $"Fox Fertility Median: {foxFertilityMedian.ToString(CultureInfo.InvariantCulture)}");
            
            UpdateStatsText(minRabbitLifeTimeText,
                $"Min Rabbit Life Time: {minRabbitLifeTime.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(averageRabbitLifeTimeText,
                $"Average Rabbit Life Time: {averageRabbitLifeTime.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(maxRabbitLifeTimeText,
                $"Max Rabbit Life Time: {maxRabbitLifeTime.ToString(CultureInfo.InvariantCulture)}");
            
            UpdateStatsText(minFoxLifeTimeText,
                $"Min Fox Life Time: {minFoxLifeTime.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(averageFoxLifeTimeText,
                $"Average Fox Life Time: {averageFoxLifeTime.ToString(CultureInfo.InvariantCulture)}");
            UpdateStatsText(maxFoxLifeTimeText,
                $"Max Fox Life Time Text: {maxFoxLifeTime.ToString(CultureInfo.InvariantCulture)}");
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
                                $"{foxFertilityMedian.ToString(CultureInfo.InvariantCulture)}," +
                                
                                $"{minRabbitLifeTime.ToString(CultureInfo.InvariantCulture)}," +
                                $"{averageRabbitLifeTime.ToString(CultureInfo.InvariantCulture)}," +
                                $"{maxRabbitLifeTime.ToString(CultureInfo.InvariantCulture)}," +
                                
                                $"{minFoxLifeTime.ToString(CultureInfo.InvariantCulture)}," +
                                $"{averageFoxLifeTime.ToString(CultureInfo.InvariantCulture)}," +
                                $"{maxFoxLifeTime.ToString(CultureInfo.InvariantCulture)}");
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

    private static void UpdateStatsText(Text textElement, string text)
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

    private (int minLifeTime, float averageLifeTime, int maxLifeTime) GetLifeTimeOfAgents(Transform container)
    {
        var lifeTimes = new List<int>();
        foreach (Transform agent in container)
        {
            lifeTimes.Add(agent.GetComponent<MovementAgent>().LifeTime);
        }

        return (lifeTimes.Min(), (float) lifeTimes.Average(), lifeTimes.Max());
    }

    private float GetMedianOfFeature(string featureName, Transform agentsContainer)
    {
        var features = new List<int>();
        foreach (Transform agent in agentsContainer)
        {
            features.Add(agent.GetComponent<Features>()[featureName]);
        }

        return GetMedianFromList(features);
    }

    private static float GetMedianFromList(IReadOnlyCollection<int> list)
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