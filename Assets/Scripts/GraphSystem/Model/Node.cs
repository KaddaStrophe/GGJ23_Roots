using Assets.Scripts.GraphSystem;
using Assets.Scripts.GraphSystem.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Textadventure/Node")]
public class Node : ScriptableObject
{
    [Multiline]
    public string text;

    public A_OutcomeDecisionHandler outcomeDecisionHandler;

    public List<string> outcomesNames = new();
    public List<Node> outcomesNodes = new();

    [HideInInspector]
    public List<Outcome> outcomes = new();

    [HideInInspector]
    public List<Outcome> selectedOutcomes;

    public void Outcome()
    {
        for (int i = 0; i < outcomesNames.Count; i++)
            outcomes.Add(new Outcome(outcomesNames[i], outcomesNodes[i]));
    }
}
