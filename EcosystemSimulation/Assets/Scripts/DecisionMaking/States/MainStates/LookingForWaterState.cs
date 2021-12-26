namespace DecisionMaking.States
{
    public class LookingForWaterState : MainState
    {
        private Needs _needs;

        private void Awake()
        {
            _needs = GetComponentInParent<Needs>();
        }
        
        public override float CurrentRank => scoreCurve.Evaluate(_needs["Thirst"]);

    }
}