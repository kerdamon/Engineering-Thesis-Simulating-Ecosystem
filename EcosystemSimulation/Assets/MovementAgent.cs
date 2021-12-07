using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MovementAgent : Agent
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turningSpeed;   
    
    private Rigidbody _agentRigidbody;

    private bool _wantInteraction = false;

    public override void Initialize()
    {
        _agentRigidbody = GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var localVelocity = transform.InverseTransformDirection(_agentRigidbody.velocity);
        sensor.AddObservation(localVelocity.x);
        sensor.AddObservation(localVelocity.z);
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        MoveAgent(actions);
        Interact(actions);
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
        rotateDir = -transform.up * rotate;


        _agentRigidbody.AddForce(dirToGo * movementSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turningSpeed);

        if (_agentRigidbody.velocity.sqrMagnitude > 25f) // slow it down
        {
            _agentRigidbody.velocity *= 0.95f;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (_wantInteraction)
        {
            //_interactor.collision.gameObject(collision.gameObject);
        }
    }

    private void Interact(ActionBuffers actions)
    {
        _wantInteraction = actions.DiscreteActions[0] > 0;
    }
}
