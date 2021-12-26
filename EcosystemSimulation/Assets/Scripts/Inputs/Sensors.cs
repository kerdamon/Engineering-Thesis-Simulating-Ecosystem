using System;
using UnityEngine;

namespace Input
{
    public class Sensors : MonoBehaviour
    {
        private Features _features;

        private void Start()
        {
            _features = GetComponent<Features>();
        }

        public GameObject ClosestFoodPositionInSensoryRange()
        {
            return ClosestGameObjectWithTagWithinSensoryRange("Food");
        }

        public GameObject ClosestPartnerPositionInSensoryRange()
        {
            var potentialPartner = ClosestGameObjectWithTagWithinSensoryRange(gameObject.tag);
            if (potentialPartner.GetComponent<GenderController>().isFemale !=
                gameObject.GetComponent<GenderController>().isFemale)
                return potentialPartner;
            throw new TargetNotFoundException();
        }

        private GameObject ClosestGameObjectWithTagWithinSensoryRange(string tagName)
        {
            var gos = GameObject.FindGameObjectsWithTag(tagName);
            GameObject closest = null;
            var distance = Mathf.Infinity;
            var position = transform.position;
            foreach (var go in gos)
            {
                if (!ObjectIsWithinSensoryRange(go))
                    continue;
                if (go == gameObject)
                    continue;
                var diff = go.transform.position - position;
                var curDistance = diff.sqrMagnitude;
                if (!(curDistance < distance))
                    continue;
                closest = go;
                distance = curDistance;
            }

            if (closest is null)
                throw new TargetNotFoundException();
            return closest;
        }

        private bool ObjectIsWithinSensoryRange(GameObject go)
        {
            return Math.Abs((transform.position - go.transform.position).magnitude) < _features["SensoryRange"];
        }
    }

    public class TargetNotFoundException : Exception
    {
    }
}