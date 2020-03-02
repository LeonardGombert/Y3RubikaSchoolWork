using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform nextCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Agent")
        {
            Agent touchedAgent = other.GetComponent<Agent>();

            if (touchedAgent.nextCheckpoint == transform)
            {
                touchedAgent.CheckpointReached(nextCheckpoint);
            }
        }
    }
}
