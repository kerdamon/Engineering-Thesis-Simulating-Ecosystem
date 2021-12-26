using Unity.Barracuda;
using Unity.MLAgents;
using UnityEngine;

public class ModelSwapper : MonoBehaviour
{
    [SerializeField] private NNModel eatingCarrotModel;

    private Agent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<MovementAgent>();
    }

    private void Update()
    {
        _agent.SetModel("SimulationAgentMovement", eatingCarrotModel);
    }
}
