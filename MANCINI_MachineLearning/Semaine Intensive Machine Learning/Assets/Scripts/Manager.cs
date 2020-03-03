using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int populationSize = 100;
    public float trainingDuration = 10;
    public float mutationRate = 1;

    public Agent agentPrefab;
    public Transform agentGroup;

    Agent agent;

    public List<Agent> agents = new List<Agent>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        NewGeneration();
        //InitNeuralNetworkViewer();
        Focus();
        yield return new WaitForSeconds(trainingDuration);
        StartCoroutine(Loop());
    }
    
    IEnumerator Loop()
    {
        NewGeneration();
        Focus();
        yield return new WaitForSeconds(trainingDuration);
        StartCoroutine(Loop());
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

            if (agents.Count <= populationSize) for (int i = 0; i < diff; i++) AddAgent();
            if (agents.Count >= populationSize) for (int i = 0; i < diff; i++) RemoveAgent();
        }
    }

    private void AddAgent()
    {
        agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity, agentGroup);
        agent.net = new NeuralNetwork(agent.net.layers);

        agents.Add(agent);
    }

    private void RemoveAgent()
    {
        //Destroy the object at the end of the list
        Destroy(agents[agents.Count - 1]);
        agents.RemoveAt(agents.Count - 1);
    }

    //Mutate the bottom half of the list (50 "worst" performing agents)
    private void Mutate()
    {
        //for each agent in the bottom half (agents.Count/2)...
        for (int i = agents.Count/2; i < agents.Count; i++)
        {
            //...copy the axon/Neural Network of the corresponding agent in the first half (51st copies 1st, 52nd copies 2nd...)
            agents[i].net.CopyNet(agents[i - agents.Count/2].net);
            agents[i].net.Mutate(mutationRate);
            agents[i].SetMutatedColor();
        }
    }

    private void ResetAgent()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].ResetAgent();
        }
    }

    private void SetColor()
    {
        //start the for loop at the second element (agents[1])
        for (int i = 1; i < agents.Count/2; i++)
            agents[i].SetDefaultColor();

        //set the color of the agent in first place
        agents[0].SetFirstColor();
    }
    private void Focus()
    {
        //NeuralNetworkViewer.instance.agent = agents[0];
        //NeuralNetworkViewer.instance.RefreshAxon();

        CameraController.instance.target = agents[0].transform;
    }
}
