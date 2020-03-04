using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maxence;

namespace Maxence
{
    public class CheckPoint : MonoBehaviour
    {
        public Transform nextCheckpoint;

        void OnTriggerEnter(Collider other)
        {
            Agent agent = other.GetComponent<Agent>();

            if (agent)
            {
                if (agent.nextCheckpoint == transform)
                {
                    agent.CheckPointReached(nextCheckpoint);
                }
            }
        }
    }
}
