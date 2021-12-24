using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predatorChcker : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"Checker");
        if (other.gameObject.CompareTag("Fox"))
        {
            Debug.Log($"Checker: sees predator");
        }
    }
}
