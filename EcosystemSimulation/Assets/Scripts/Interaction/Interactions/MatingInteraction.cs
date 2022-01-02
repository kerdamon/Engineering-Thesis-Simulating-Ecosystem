using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interaction
{
    public class MatingInteraction : Interaction
    {
        [SerializeField] private int averageOffspringNumberPerLitter;
        [SerializeField] private int maxDeviationFromFertility;
        [SerializeField] private int maxRandomDeviation;
        [SerializeField] private float mutationProbability;

        [SerializeField] private GameObject maleRabbitChild;
        [SerializeField] private GameObject femaleRabbitChild;

        private Needs _needs;
        private Features _features;

        private int _maxFeatureValue;
        private int _minFeatureValue = 0;   //todo change, magic number

        protected override void Start()
        {
            base.Start();
            _needs = GetComponentInParent<Needs>();
            _features = GetComponentInParent<Features>();
            _maxFeatureValue = (int)_features.maxValue;
        }

        protected override void AtInteractionEnd()
        {
            var mateNeeds = SecondSimulationObject.GetComponent<Needs>();
            _needs["ReproductionUrge"] = 0;
            mateNeeds["ReproductionUrge"] = 0;
            SpawnOffspring(SecondSimulationObject);
        }

        private void SpawnOffspring(GameObject mate)
        {
            var actorFeatures = SimulationObject.GetComponent<Features>();
            var mateFeatures = mate.GetComponent<Features>();

            var numberOfChildren = CalculateNumberOfOffspring(mateFeatures["Fertility"]);
            for (var i = 0; i < numberOfChildren; i++)
            {
                var originalGameObject = Random.value > 0.5f ? maleRabbitChild : femaleRabbitChild;
                var offspring = Instantiate(originalGameObject, transform.parent.parent);
                offspring.transform.position = mate.transform.position;
                offspring.transform.Translate(Random.value * 2, 0, Random.value * 2);
                var offspringFeatures = offspring.GetComponent<Features>();
                
                //crossover
                foreach (var f in actorFeatures)
                {
                    offspringFeatures[f.Key] = Random.value > 0.5f ? f.Value : mateFeatures[f.Key];
                }

                //mutation
                foreach (var f in actorFeatures)
                {
                    var feature = f.Key;
                    offspringFeatures[feature] = Random.value < mutationProbability
                        ? Random.Range(0, 101)
                        : offspringFeatures[feature];
                } 
            }
        }

        private int CalculateNumberOfOffspring(int fertility)
        {
            return averageOffspringNumberPerLitter + CalculateNumberOfOffspringFromFertility(fertility) + CalculateRandomChildrenComponent();
        }
        
        private int CalculateNumberOfOffspringFromFertility(int fertility)
        {
            var wholeLength = _maxFeatureValue - _minFeatureValue;
            //Debug.Log($"wholeLength {wholeLength}");
            var numberOfRanges = maxDeviationFromFertility * 2 + 1;
            //Debug.Log($"numberOfRanges {numberOfRanges}");
            var rangeLength = wholeLength * 1.0f / numberOfRanges;
            //Debug.Log($"rangeLength {rangeLength}");
            var returned = (int)(fertility / rangeLength) - maxDeviationFromFertility;
            //Debug.Log($"returned {returned}");

            return returned;
        }

        private int CalculateRandomChildrenComponent()
        {
            var returned = Random.Range(-maxRandomDeviation, maxRandomDeviation+1);
            //Debug.Log($"Random childeren component: {returned}");
            return returned;
        }
        
        private static int MutateFeature(int feature, float probability, string featureName)
        {
            while(true)
            {
                var featureInBinaryRepresentation = ConvertFeatureToBinaryRepresentation(feature);
                var featureBinOld = BitArrayToString(featureInBinaryRepresentation);
                for (int i = 0; i < 7; i++)
                {
                    if (Random.value <= probability)
                    {
                        featureInBinaryRepresentation[i] = !featureInBinaryRepresentation[i];
                    }
                }
                var mutatedFeature = ByteArrayToInt(BitArrayToByteArray(featureInBinaryRepresentation));

                if (mutatedFeature <= 100)
                {
                    //if(BitArrayToString(featureInBinaryRepresentation) != featureBinOld)
                        //Debug.Log($"Zmutowano {featureName} Old dec: {feature} bin: {featureBinOld}, new dec: {mutatedFeature} bin: {BitArrayToString(featureInBinaryRepresentation)}");
                    return mutatedFeature;
                }

                //Debug.Log($"Zmutowano cechę ponad 100, aktualna wartość: dec: {mutatedFeature}, bin: {BitArrayToString(featureInBinaryRepresentation)}");
            }
        }

        private static BitArray ConvertFeatureToBinaryRepresentation(int feature)
        {
            return new BitArray(BitConverter.GetBytes(feature));
        }
       
        private static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }
        
        private static int ByteArrayToInt(byte[] byteArray)
        {
            return BitConverter.ToInt32(byteArray, 0);
        }

        private static string BitArrayToString(BitArray bitArray)
        {
            var bitArrayAsString = "";
            for(var i = bitArray.Length - 1; i >= 0; i--)
            {
                var bit = bitArray[i];
                bitArrayAsString += bit ? "1" : "0";
            }

            return bitArrayAsString;
        }
    }
}
