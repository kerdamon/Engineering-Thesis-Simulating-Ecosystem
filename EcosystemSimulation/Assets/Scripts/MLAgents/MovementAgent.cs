using System;
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
    private bool _isTraining;

    public bool WantInteraction { get; private set; } = false;

    public override void Initialize()
    {
        _agentRigidbody = GetComponent<Rigidbody>();
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
        MoveAgent(actions);
        GetInteractDesire(actions);
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


        _agentRigidbody.AddForce(dirToGo * movementSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turningSpeed);
        
        if (_agentRigidbody.velocity.sqrMagnitude > 25f) // slow it down
        {
            _agentRigidbody.velocity *= 0.95f;
        }
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
        RandomizePositionAndRotationWithCollisionCheck(transform1, transform1.parent);
    }
    
    //todo abstract to other place, this is just copied from TrainingArea Script, but is even worse, with magic numbers
    void RandomizePositionAndRotationWithCollisionCheck(Transform obj, Transform containterTransform)
    {
        var iterator = 0;
        var newPosition = obj.position;
        var newRotation = obj.rotation;
        while (iterator < 50)
        {
            newPosition = containterTransform.TransformPoint(new Vector3(Random.Range(-80, 80), obj.localPosition.y, Random.Range(-80, 80)));
            newRotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            var isCollision = Physics.CheckBox(newPosition, obj.localScale / 2);
            if(!isCollision)
            {
                break;
            }
            iterator++;

        }
        obj.position = newPosition;
        obj.rotation = newRotation;
    }

    public void KillAgent(string deathCause)
    {
        //todo expand to state
        Debug.Log($"Agent {gameObject.name} died of {deathCause}");
        Destroy(gameObject);
    }
}