using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateVisualizer : MonoBehaviour
{
    private TextMesh _tooltip;
    private StateMachine _state;

    private void Start()
    {
        _tooltip = GetComponentInChildren<TextMesh>();
        _state = GetComponent<StateMachine>();
    }

    void Update()
    {
        _tooltip.text = _state.CurrentState.ToString();
    }
}
