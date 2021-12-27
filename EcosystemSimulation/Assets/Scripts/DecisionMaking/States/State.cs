using Interaction;
using Interaction.InteractionManagers;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Policies;
using UnityEngine;

namespace DecisionMaking.States
{
    /// <summary>
    /// State only indicates in which state agent is. State objects does not contain any logic other than determining its score and preparing movement model.
    /// </summary>
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private NNModel nnModel;
        private Agent _agent;
        private string _behaviourName;

        protected DrinkingInteraction DrinkingInteraction;

        protected virtual void Start()
        {
            _agent = GetComponentInParent<MovementAgent>();
            _behaviourName = GetComponentInParent<BehaviorParameters>().BehaviorName;    //todo change if there are multiple behaviours on one agent
            var parent = transform.parent;

            DrinkingInteraction = parent.parent.GetComponentInChildren<DrinkingInteraction>();
        }

        public void PrepareModel()
        {
            _agent.SetModel(_behaviourName, nnModel);
        }
        public abstract float CurrentRank { get; }

        public virtual void OnEnterState()
        {
        }
        
        public virtual void OnLeaveState()
        {
        }
    }
}