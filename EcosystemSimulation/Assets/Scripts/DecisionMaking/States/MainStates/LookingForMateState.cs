using DecisionMaking.States.SpecialStates;
using Interaction;
using UnityEngine;

namespace DecisionMaking.States
{
    public class LookingForMateState : MainState
    {
        [SerializeField] private float increaseInPartnersUrgeOnMatingAttempt;
        public override float CurrentRank => scoreCurve.Evaluate(Needs["ReproductionUrge"]);

        private MatingState _thisMatingState;
        private MatingState _partnerMatingState;
        
        protected override void Start()
        {
            base.Start();
            _thisMatingState = transform.parent.GetComponentInChildren<MatingState>();
            MatingInteraction.BeforeInteraction += () =>
            {
                _thisMatingState.ActivateThis();
                _partnerMatingState.ActivateThis();
            };
            MatingInteraction.AfterInterruptedInteraction += () =>
            {
                _thisMatingState.DeactivateThis();
                _partnerMatingState.DeactivateThis();
            };
            MatingInteraction.AfterSuccessfulInteraction += () =>
            {
                _thisMatingState.DeactivateThis();
                _partnerMatingState.DeactivateThis();
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            var mateNeeds = other.GetComponent<Needs>();
            if (!enabled || !other.gameObject.CompareTag("Rabbit-Female")) return;
            if (mateNeeds.IsMaxOrGreater("ReproductionUrge"))
            {
                Mate = other.transform.parent.gameObject;
                _partnerMatingState = Mate.GetComponentInChildren<MatingState>();
                InteractionManager.InteractIfAbleWith(MatingInteraction, Mate);
            }
            else
            {
                mateNeeds["ReproductionUrge"] += increaseInPartnersUrgeOnMatingAttempt;
            }
        }
    }
}