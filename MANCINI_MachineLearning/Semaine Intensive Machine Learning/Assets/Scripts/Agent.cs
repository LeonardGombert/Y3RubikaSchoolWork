using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leonard;

namespace Leonard
{
    public class Agent : MonoBehaviour, IComparable<Agent>
    {
        public NeuralNetwork net;
        //the higher this value is, the better (total distance)
        public float fitness;

        public Transform nextCheckpoint;
        public float nextCheckpointDist;
        //distance since last checkpoint (different from fitness)
        public float distanceTraveled;

        public MeshRenderer render;

        public Material defaultMat;
        public Material mutantMat;
        public Material firstMat;

        public Transform tr;
        public Rigidbody rb;

        public float[] inputs;

        Vector3 pos;
        RaycastHit hit;

        public float rayRange = 1;

        public LayerMask layerMask;

        public CarController carController;

        public void CheckpointReached(Transform newNextCheckpoint)
        {
            distanceTraveled += nextCheckpointDist;
            nextCheckpoint = newNextCheckpoint;
            nextCheckpointDist = (transform.position - nextCheckpoint.position).magnitude;
        }

        public void SetDefaultColor()
        {
            render.material = defaultMat;
        }

        public void SetMutatedColor()
        {
            render.material = mutantMat;
        }

        public void SetFirstColor()
        {
            render.material = firstMat;
        }

        public int CompareTo(Agent other)
        {
            if (fitness < other.fitness)
            {
                return 1;
            }

            else if (fitness > other.fitness)
            {
                return -1;
            }

            else return 0;
        }

        internal void ResetAgent()
        {
            fitness = 0;
            tr.position = Vector3.zero;
            tr.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            distanceTraveled = 0;

            nextCheckpoint = CheckpointManager.instance.firstCheckPoint;
            nextCheckpointDist = (tr.position - nextCheckpoint.position).magnitude;

            inputs = new float[net.layers[0]];
        }

        private void FixedUpdate()
        {
            InputUpdate();
            OutputUpdate();
            UpdateFitness();
        }


        private void InputUpdate()
        {
            pos = transform.position;
            inputs[0] = RaySensor(pos + new Vector3(0, .2f, 0), transform.forward, 4);
            inputs[1] = RaySensor(pos + new Vector3(0, .2f, 0), transform.right + transform.forward, 2);
            inputs[2] = RaySensor(pos + new Vector3(0, .2f, 0), -transform.right + transform.forward, 2);
            inputs[3] = RaySensor(pos + new Vector3(0, .2f, 0), transform.right, 1);
            inputs[4] = RaySensor(pos + new Vector3(0, .2f, 0), -transform.right, 1);

            //horizontal and vertical inputs
            inputs[5] = 1 - (float)Math.Tanh(rb.velocity.magnitude / 20);
            inputs[6] = (float)Math.Tanh(rb.angularVelocity.y * 0.01f);
        }
        private void OutputUpdate()
        {
            net.FeedForward(inputs);

            //find the last layer (x value, horizontalInput) and the first y value (node 0)
            carController.horizontalInput = net.neurons[net.layers.Length - 1][0];
            //find the last layer (x value, verticalInput) and the second y value (node 1)
            carController.verticalInput = net.neurons[net.layers.Length - 1][1];
        }

        void UpdateFitness()
        {
            SetFitness(
                distanceTraveled + //total travelled distance
                (nextCheckpointDist - //fixed value, calculated once when you hit a new checkpoint
                (transform.position - nextCheckpoint.position).magnitude)); //incremental value, calculated as you move to next checkpoint
        }

        //saves the car's highest fitness score
        void SetFitness(float _fitness)
        {
            //if car's (base) fitness is smaller than new fitness
            if (fitness < _fitness)
            {
                //base fitness = new fitness... does not update if _fitness is smaller!! only save positive progress
                fitness = _fitness;
            }
        }

        //feeds the information into the inputs
        float RaySensor(Vector3 pos, Vector3 direction, float length)
        {
            if (Physics.Raycast(pos, direction, out hit, length * rayRange, layerMask))
            {
                Debug.DrawRay(pos, direction * length * rayRange, Color.green);
                return (rayRange * length - hit.distance) / (rayRange * length); //returns a value between 0 and one
            }

            else
            {
                Debug.DrawRay(pos, direction * length * rayRange, Color.red);
                return 0;
            }
        }
    }
}