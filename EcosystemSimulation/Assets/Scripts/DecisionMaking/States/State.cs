using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using UnityEngine;

namespace DecisionMaking.States
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private NNModel nnModel;
        private Agent _agent;
        private string _behaviourName;

        private void Start()
        {
            _agent = GetComponentInParent<MovementAgent>();
            _behaviourName = GetComponentInParent<BehaviorParameters>().BehaviorName;    //todo change if there are multiple behaviours on one agent
        }

        public void PrepareModel()
        {
            _agent.SetModel(_behaviourName, nnModel);
        }
        public abstract float CurrentRank { get; }
    }
}