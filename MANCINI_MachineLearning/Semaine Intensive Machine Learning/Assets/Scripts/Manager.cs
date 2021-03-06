﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leonard;

namespace Leonard
{
    public class Manager : MonoBehaviour
    {
        public int populationSize = 100;
        public float trainingDuration = 10;
        public float mutationRate = 1;

        public Agent agentPrefab;
        public Transform agentGroup;

        Agent agent;

        public List<Agent> agents = new List<Agent>();
        public InputField keptAgents;

        float numberOfAgentsToKeep = 1;
        public float _keptAgentsMultiplier;

        public bool dontMutate;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(InitCoroutine());
        }

        IEnumerator InitCoroutine()
        {
            _keptAgentsMultiplier = numberOfAgentsToKeep / populationSize;
            NewGeneration();
            //InitNeuralNetworkViewer();
            Focus();
            yield return new WaitForSeconds(trainingDuration);
            StartCoroutine(LoopCoroutine());
        }

        IEnumerator LoopCoroutine()
        {
            if(keptAgents.text == null) numberOfAgentsToKeep = 50;
            else numberOfAgentsToKeep = float.Parse(keptAgents.text);
            _keptAgentsMultiplier = numberOfAgentsToKeep / populationSize;
            NewGeneration();
            Focus();
            yield return new WaitForSeconds(trainingDuration);
            StartCoroutine(LoopCoroutine());
        }

        void NewGeneration()
        {
            //are sorted thanks to the IComparable interface in the agents class
            agents.Sort();
            AddOrRemoveAgent();
            Mutate();
            ResetAgent();
            SetColor();
        }

        //look at agent list, and add or remove according to population size
        private void AddOrRemoveAgent()
        {
            if (agents.Count != populationSize)
            {
                int diff = populationSize - agents.Count;

                if (agents.Count <= populationSize)
                {
                    for (int u = 0; u < diff; u++)
                    {
                        AddAgent();
                    }
                }
                else
                {
                    for (int w = 0; w < diff; w++)
                    {
                        RemoveAgent();
                    }
                }
            }
        }

        private void AddAgent()
        {
            agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity, agentGroup);
            agent.net = new NeuralNetwork(agent.net.layers);
            agents.Add(agent);
        }

        void RemoveAgent()
        {
            //Destroy the object at the end of the list
            Destroy(agents[agents.Count - 1].gameObject);
            agents.RemoveAt(agents.Count - 1);
        }

        //Mutate the bottom half of the list (50 "worst" performing agents)
        private void Mutate()
        {

            if(dontMutate)
            {
                int substractionValue = (int)(agents.Count * _keptAgentsMultiplier)/*10*/;

                //for each agent in the bottom half (agents.Count/2)...
                for (int g = (int)(agents.Count * _keptAgentsMultiplier)/*10*/; g < agents.Count; g++)
                {
                    //...copy the axon/Neural Network of the corresponding agent in the first half (51st copies 1st, 52nd copies 2nd...)
                    /*10-10 = copy 0 
                     * 11-10 = copy 1
                     * 12-10 = copy 2
                     * ...
                     * 
                     * 20-10 = copy 10
                     * 21-20 = copy 11 (?)
                     * 31-30
                     */

                    //bring to decimal value
                    //floor to lowest int value
                    //times 10

                    if (g < 21) substractionValue = Mathf.FloorToInt(g / substractionValue);
                    else if (g > 21 && g < 101) substractionValue = Mathf.FloorToInt(g / 10);


                    agents[g].net.CopyNet(agents[g - substractionValue * 10].net);
                    //agents[g].net.Mutate(mutationRate);
                    agents[g].SetMutatedColor();
                }
            }

            else
            {
                //for each agent in the bottom half (agents.Count/2)...STARTS COUNTING FROM 50
                for (int g = agents.Count / 2; g < agents.Count; g++)
                {
                    //...copy the axon/Neural Network of the corresponding agent in the first half (51st copies 1st, 52nd copies 2nd...)
                    //STARTS COPYING AT 50-50 = 0, 51-50 = 1, 52-50 = 2...
                    agents[g].net.CopyNet(agents[g - agents.Count / 2].net);
                    agents[g].net.Mutate(mutationRate);
                    agents[g].SetMutatedColor();
                }
            }
        }

        private void ResetAgent()
        {
            for (int k = 0; k < agents.Count; k++)
            {
                agents[k].ResetAgent();
            }
        }

        private void SetColor()
        {
            //start the for loop at the second element (agents[1])
            for (int s = 1; s < (int)(agents.Count * _keptAgentsMultiplier); s++)
            {
                agents[s].SetDefaultColor();
            }

            //set the color of the agent in first place
            agents[0].SetFirstColor();
        }

        private void Focus()
        {
            //NeuralNetworkViewer.instance.agent = agents[0];
            //NeuralNetworkViewer.instance.RefreshAxon();

            CameraController.instance.target = agents[0].transform;
        }

        public void Save()
        {
            List<NeuralNetwork> neuralNetworks = new List<NeuralNetwork>();

            for (int v = 0; v < agents.Count; v++)
            {
                neuralNetworks.Add(agents[v].net);
            }

            DataManager.instance.Save(neuralNetworks);
        }

        public void Load()
        {
            Data data = DataManager.instance.Load();

            if (data != null)
            {
                for (int b = 0; b < agents.Count; b++)
                {
                    agents[b].net = data.neuralNetworks[b];
                }
            }

            End();
        }

        public void End()
        {
            StopAllCoroutines();
            StartCoroutine(LoopCoroutine());
        }

        public void ResetNeuralNetworks()
        {
            for (int p = 0; p < agents.Count; p++)
            {
                agents[p].net = new NeuralNetwork(agent.net.layers);
            }

            End();
        }
    }
}