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
    [SerializeField] private GameObject statsCanvas;
    [SerializeField] private GameObject deathCanvas;
    
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
    
    [SerializeField] private Text foxesDiedOfHungerText;
    [SerializeField] private Text foxesDiedOfThirstText;
    
    [SerializeField] private Text rabbitsDiedOfHungerText;
    [SerializeField] private Text rabbitsDiedOfThirstText;
    [SerializeField] private Text rabbitsDiedOfBeingEatenText;

    [Range(1, 20)] [SerializeField] private int updatePeriod = 1;

    [Range(10, 100)] [SerializeField] private int logPeriod = 20;

    private FileLogger _fileLogger;

    public int FoxesDiedOfHunger { get; set; }
    public int FoxesDiedOfThirst { get; set; }
    public int RabbitsDiedOfHunger { get; set; }
    public int RabbitsDiedOfThirst { get; set; }
    public int RabbitsDiedOfBeingEaten { get; set; }
    
    private void Awake()
    {
        _fileLogger = GetComponent<FileLogger>();
    }

    private void Start()
    {
        _fileLogger.LogLine(
            "Timestamp,FoxesPopulation,RabbitPopulation,RabbitSpeedMedian,RabbitSensoryRangeMedian,RabbitFertilityMedian,FoxSpeedMedian,FoxSensoryRangeMedian,FoxFertilityMedian,MinRabbitLifeTime,AverageRabbitLifeTime,MaxRabbitLifeTime,MinFoxLifeTime,AverageFoxLifeTime,MaxFoxLifeTime,FoxesDiedOfHunger,FoxesDiedOfThirst,RabbitsDiedOfHunger,RabbitsDiedOfThirst,RabbitsDiedOfBeingEaten");
    }

    private void Update()
    {
        if (!ShouldLogToFile() && !ShouldUpdateStatsCanvas()) return;
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

        if (ShouldUpdateStatsCanvas())
        {
            UpdateStatsText(foxesPopulationText, $"Foxes on scene: {foxesPopulation.ToString()}");
            UpdateStatsText(rabbitsPopulationText, $"Rabbits on scene: {rabbitsPopulation.ToString()}");
            
            UpdateStatsText(rabbitSpeedMedianText,
                $"Rabbit Speed Median: {rabbitSpeedMedian.ToString()}");
            UpdateStatsText(rabbitSensoryRangeMedianText,
                $"Rabbit Sensory Range Median: {rabbitSensoryRangeMedian.ToString()}");
            UpdateStatsText(rabbitFertilityMedianText,
                $"Rabbit Fertility Median: {rabbitFertilityMedian.ToString()}");
            
            UpdateStatsText(foxSpeedMedianText,
                $"Fox Speed Median: {foxSpeedMedian.ToString()}");
            UpdateStatsText(foxSensoryRangeMedianText,
                $"Fox Sensory Range Median: {foxSensoryRangeMedian.ToString()}");
            UpdateStatsText(foxFertilityMedianText,
                $"Fox Fertility Median: {foxFertilityMedian.ToString()}");
            
            UpdateStatsText(minRabbitLifeTimeText,
                $"Min Rabbit Life Time: {minRabbitLifeTime.ToString()}");
            UpdateStatsText(averageRabbitLifeTimeText,
                $"Average Rabbit Life Time: {averageRabbitLifeTime.ToString()}");
            UpdateStatsText(maxRabbitLifeTimeText,
                $"Max Rabbit Life Time: {maxRabbitLifeTime.ToString()}");
            
            UpdateStatsText(minFoxLifeTimeText,
                $"Min Fox Life Time: {minFoxLifeTime.ToString()}");
            UpdateStatsText(averageFoxLifeTimeText,
                $"Average Fox Life Time: {averageFoxLifeTime.ToString()}");
            UpdateStatsText(maxFoxLifeTimeText,
                $"Max Fox Life Time Text: {maxFoxLifeTime.ToString()}");
        }
        
        if (ShouldUpdateDeathCanvas())
        {
            UpdateStatsText(foxesDiedOfHungerText,
                $"Foxes that died of hunger: {FoxesDiedOfHunger.ToString()}");
            UpdateStatsText(foxesDiedOfThirstText,
                $"Foxes that died of thirst: {FoxesDiedOfThirst.ToString()}");
            
            UpdateStatsText(rabbitsDiedOfHungerText,
                $"Rabbits that died of hunger: {RabbitsDiedOfHunger.ToString()}");
            UpdateStatsText(rabbitsDiedOfThirstText,
                $"Rabbits that died of thirst: {RabbitsDiedOfThirst.ToString()}");
            UpdateStatsText(rabbitsDiedOfBeingEatenText,
                $"Rabbits died of being eaten: {RabbitsDiedOfBeingEaten.ToString()}");
        }

        if (ShouldLogToFile())
        {
            _fileLogger.LogLine($"{Academy.Instance.StepCount.ToString()}," +
                                $"{foxesPopulation.ToString()}," +
                                $"{rabbitsPopulation.ToString()}," +
                                
                                $"{rabbitSpeedMedian.ToString()}," +
                                $"{rabbitSensoryRangeMedian.ToString()}," +
                                $"{rabbitFertilityMedian.ToString()}," +
                                
                                $"{foxSpeedMedian.ToString()}," +
                                $"{foxSensoryRangeMedian.ToString()}," +
                                $"{foxFertilityMedian.ToString()}," +
                                
                                $"{minRabbitLifeTime.ToString()}," +
                                $"{averageRabbitLifeTime.ToString()}," +
                                $"{maxRabbitLifeTime.ToString()}," +
                                
                                $"{minFoxLifeTime.ToString()}," +
                                $"{averageFoxLifeTime.ToString()}," +
                                $"{maxFoxLifeTime.ToString()}," +
                                
                                $"{FoxesDiedOfHunger.ToString()}," +
                                $"{FoxesDiedOfThirst.ToString()}," +
                
                                $"{RabbitsDiedOfHunger.ToString()}," +
                                $"{RabbitsDiedOfThirst.ToString()}," +
                                $"{RabbitsDiedOfBeingEaten.ToString()}"
                                );
        }
    }

    private bool ShouldLogToFile()
    {
        return Time.frameCount % logPeriod == 0;
    }

    private bool ShouldUpdateStatsCanvas()
    {
        return Time.frameCount % updatePeriod == 0 && statsCanvas.activeSelf;
    }
    
    private bool ShouldUpdateDeathCanvas()
    {
        return Time.frameCount % updatePeriod == 0 && deathCanvas.activeSelf;
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

    private static (int? minLifeTime, float? averageLifeTime, int? maxLifeTime) GetLifeTimeOfAgents(Transform container)
    {
        if (CountAgents(container) <= 0) return (null, null, null);
        var lifeTimes = new List<int>();
        foreach (Transform agent in container)
        {
            lifeTimes.Add(agent.GetComponent<MovementAgent>().LifeTime);
        }

        return (lifeTimes.Min(), (float) lifeTimes.Average(), lifeTimes.Max());
    }

    private float? GetMedianOfFeature(string featureName, Transform agentsContainer)
    {
        if (CountAgents(agentsContainer) <= 0) return null;
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