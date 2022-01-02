using System;
using NaughtyAttributes;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovementAgent : Agent
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turningSpeed;
    
    private Rigidbody _agentRigidbody;
    private Features _features;
    
    private bool _isTraining;
    private TrainingArea _trainingArea;
    
    [ShowNativeProperty] public int LifeTime { get; private set; }
    
    public bool WantInteraction { get; private set; }

    private void Update()
    {
        LifeTime++;
    }

    public override void Initialize()
    {
        _features = GetComponent<Features>();
        _agentRigidbody = GetComponent<Rigidbody>();
        _trainingArea = GetComponentInParent<TrainingArea>();
        _isTraining = Mathf.Abs(Academy.Instance.EnvironmentParameters.GetWithDefault("is_training", 0.0f)) > 0.0001f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var localVelocity = transform.InverseTransformDirection(_agentRigidbody.velocity);
        sensor.AddObservation(localVelocity.x);
        sensor.AddObservation(localVelocity.z);
    }

    public Action AfterAction;
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log($"{actions.DiscreteActions[0]}", this);
        MoveAgent(actions);
        GetInteractDesire(actions);
        //Debug.Log($"Ustawiam {WantInteraction}, bo dics.acts[0] = {actions.DiscreteActions[0]}", this);
        ModifyRewardOnActionReceived();
        AfterAction();
    }

    protected virtual void ModifyRewardOnActionReceived()
    {
    }
    
    public void MoveAgent(ActionBuffers actions)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var continuousActions = actions.ContinuousActions;

        var forward = Mathf.Clamp(continuousActions[0], -1f, 1f);
        var right = Mathf.Clamp(continuousActions[1], -1f, 1f);
        var rotate = Mathf.Clamp(continuousActions[2], -1f, 1f);
        
        dirToGo = transform.forward * forward;
        dirToGo += transform.right * right;
        rotateDir = transform.up * rotate;

        var featuresFactor = CalculateFeaturesFactor(_features["Speed"]);
        _agentRigidbody.AddForce(dirToGo * movementSpeed * featuresFactor, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turningSpeed);
        
        if (_agentRigidbody.velocity.sqrMagnitude > 25f) // slow it down
        {
            _agentRigidbody.velocity *= 0.95f;
        }
    }

    private static float CalculateFeaturesFactor(int featureValue)
    {
        return Mathf.Pow(1.0139598f, featureValue - 50);
    }

    private void GetInteractDesire(ActionBuffers actions)
    {
        WantInteraction = actions.DiscreteActions[0] > 0;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            continuousActionsOut[2] = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            continuousActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            continuousActionsOut[2] = -1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            continuousActionsOut[0] = -1;
        }
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    public override void OnEpisodeBegin()
    {
        if (!_isTraining) return;
        var transform1 = transform;
        _trainingArea.RandomizePositionAndRotationWithCollisionCheck(transform1, transform1.parent);
    }
    
    public void KillAgent(string deathCause)
    {
        //todo expand to state
        Debug.Log($"Agent {gameObject.name} died of {deathCause}");
        Destroy(gameObject);
    }
}