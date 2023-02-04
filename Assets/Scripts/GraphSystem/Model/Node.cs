using Assets.Scripts.GraphSystem;
using Assets.Scripts.GraphSystem.Model;
using Assets.Scripts.GraphSystem.Model.OutcomeByUserHandler;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Textadventure/Node")]
public class Node : ScriptableObject
{
    // narrative info

    [Multiline]
    public string content;
    public Speaker speaker;
    public Depth depth;
    public bool isStartOfScene;
    public string[] backgroundFlavorContent;

    // logic

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
            var outcome  = ScriptableObject.CreateInstance<Outcome>();
            outcome.content  = outcomesNames[i];
            outcome.nextNode = outcomesNodes[i];
            outcomes.Add(outcome);
        }
    }

    public bool IsDecision() {
        return outcomeDecisionHandler.GetType() == typeof(OutcomeByUserHandler);
    }
}
