using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leonard;

namespace Leonard
{
    public class Checkpoint : MonoBehaviour
    {
        public Transform nextCheckpoint;

        private void OnTriggerEnter(Collider other)
        {
            Agent touchedAgent = other.GetComponent<Agent>();

            if (touchedAgent)
            {
                if (touchedAgent.nextCheckpoint == transform)
                {
                    touchedAgent.CheckpointReached(nextCheckpoint);
                }
            }
        }
    }
}