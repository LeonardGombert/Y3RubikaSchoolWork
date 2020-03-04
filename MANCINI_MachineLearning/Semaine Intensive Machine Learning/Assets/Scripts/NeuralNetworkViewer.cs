using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leonard;

namespace Leonard
{
    public class NeuralNetworkViewer : MonoBehaviour
    {
        public static NeuralNetworkViewer instance;

        public Gradient colorGradient;
        const float decalX = 100;
        const float decalY = 20;

        public Transform viewGroup;

        public RectTransform neuronPrefab;
        public RectTransform axonPrefab;
        public RectTransform fitnessPrefab;
        RectTransform fitness;
        public Agent agent;

        Image[][] neurons;
        Text[][] neuronsValue;
        Image[][][] axons;

        RectTransform neuron;
        RectTransform axon;
        Text FItnessText;

        private int myInt;

        private void Awake()
        {
            if (instance != null)
                Destroy(instance);

            else instance = this;
        }

        private void Init(Agent _agent)
        {
            agent = _agent;
            CreateViewer(agent.net);
        }

        void CreateViewer(NeuralNetwork net)
        {
            for (int i = viewGroup.childCount - 1; i > -1; i--)
            {
                DestroyImmediate(viewGroup.GetChild(i).gameObject);
            }
        }
    }
}