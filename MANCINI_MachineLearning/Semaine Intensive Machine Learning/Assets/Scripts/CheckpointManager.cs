using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leonard;

namespace Leonard
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager instance;

        public Transform firstCheckPoint;

        // Start is called before the first frame update
        void Awake()
        {
            if (instance != null)
                Destroy(instance);

            else instance = this;
            Init();
        }

        [ContextMenu("Init Checkpoint")]
        public void Init()
        {
            firstCheckPoint = transform.GetChild(0);

            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(i + 1);
            }

            transform.GetChild(transform.childCount - 1).GetComponent<Checkpoint>().nextCheckpoint = transform.GetChild(0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}