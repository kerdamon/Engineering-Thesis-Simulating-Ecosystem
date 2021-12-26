using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Interaction.Interactions
{
    public class MatingInteraction : Interaction
    {
        [SerializeField] private int maxChildrenPerLitter;
        [SerializeField] private float mutationProbability;
        
        protected override void AtInteractionEnd()
        {
            var mate = SecondSimulationObject.transform.parent.gameObject;
            SpawnOffspring(mate);
        }

        private void SpawnOffspring(GameObject mate)
        {
            var numberOfChildren = Random.value * maxChildrenPerLitter;
            for (var i = 0; i < numberOfChildren; i++)
            {
                var originalGameObject = Random.value > 0.5f ? gameObject.transform.parent.gameObject : mate;
                var offspring = Instantiate(originalGameObject, transform.parent.parent);
                offspring.transform.Translate(Random.value * 2, 0, Random.value * 2);
                var offspringFeatures = offspring.GetComponent<Features>();
                var actorFeatures = SimulationObject.GetComponent<Features>();
                var mateFeatures = mate.GetComponent<Features>();
                
                //crossover
                foreach (var f in actorFeatures)
                {
                    offspringFeatures[f.Key] = Random.value > 0.5f ? f.Value : mateFeatures[f.Key];
                }

                //mutation
                foreach (var f in actorFeatures)
                {
                    var feature = f.Key;
                    offspringFeatures[feature] = MutateFeature(offspringFeatures[feature], mutationProbability, feature);
                } 
            }
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
