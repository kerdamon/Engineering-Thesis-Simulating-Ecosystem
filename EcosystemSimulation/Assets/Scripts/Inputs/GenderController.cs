using UnityEngine;

namespace Input
{
    public class GenderController: MonoBehaviour
    {
        [SerializeField] public bool isFemale;
        [SerializeField] public float pregnancyTimeLeft;
        public bool IsPregnant => pregnancyTimeLeft > 0;
    }
}
