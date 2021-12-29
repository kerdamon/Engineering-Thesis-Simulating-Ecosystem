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
        private Needs _mateNeeds;
        
        protected override void Awake()
        {
            base.Awake();
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
            if (!enabled || !other.gameObject.CompareTag("Rabbit-Female")) return;
            Mate = other.transform.parent.gameObject;
            _mateNeeds = Mate.GetComponent<Needs>();
            _partnerMatingState = Mate.GetComponentInChildren<MatingState>();
            if (_mateNeeds.IsMaxOrGreater("ReproductionUrge"))
            {
                InteractionManager.InteractIfAbleWith(MatingInteraction, Mate);
            }
            else
            {
                _mateNeeds["ReproductionUrge"] += increaseInPartnersUrgeOnMatingAttempt;
            }
        }
    }
}