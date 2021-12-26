namespace DecisionMaking.States
{
    public class LookingForMateState : MainState
    {
        private Needs _needs;

        private void Awake()
        {
            _needs = GetComponentInParent<Needs>();
        }
        
        public override float CurrentRank => scoreCurve.Evaluate(_needs["ReproductionUrge"]);

    }
}