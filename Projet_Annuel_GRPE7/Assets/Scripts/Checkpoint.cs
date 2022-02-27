using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Circuit circuit;
    public int checkPointNumber;

    private void Start()
    {
        circuit = GetComponentInParent<Circuit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var p = other.GetComponentInParent<Player>();
        circuit.checkpointPassed(p, checkPointNumber);
    }
}
