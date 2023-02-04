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
    public string content;

    public A_OutcomeDecisionHandler outcomeDecisionHandler;

    public List<string> outcomesNames = new List<string>();
    public List<Node>   outcomesNodes = new List<Node>();

    [HideInInspector]
    public List<Outcome> outcomes = new List<Outcome>();

    [HideInInspector]
    public List<Outcome> selectedOutcomes;

    public void Outcome()
    {
        for (int i = 0; i < outcomesNames.Count; i++) {
            outcomes.Add(new Outcome(outcomesNames[i], outcomesNodes[i]));
        }
    }
}
