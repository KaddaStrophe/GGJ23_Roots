using Assets.Scripts.GraphSystem;
using Assets.Scripts.GraphSystem.Model;
using Assets.Scripts.GraphSystem.Model.OutcomeByUserHandler;
using Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Textadventure/Node")]
public class Node : ScriptableObject
{
    // narrative info

    [SerializeField, Multiline]
    public string content;
    [SerializeField]
    public Speaker speaker;
    [SerializeField]
    public Depth depth;
    [SerializeField]
    public bool isStartOfScene;
    [SerializeField]
    public string backgroundFlavorContent;

    // logic

    [SerializeField]
    public A_OutcomeDecisionHandler outcomeDecisionHandler;

    [SerializeField]
    public List<string> outcomesNames = new List<string>();
    [SerializeField]
    public List<Node>   outcomesNodes = new List<Node>();

    [SerializeField, HideInInspector]
    public List<Outcome> outcomes = new List<Outcome>();

    [HideInInspector]
    public List<Outcome> selectedOutcomes;

    public void Init() {

        // Debug.Log("init node " + this + " outcome count " + outcomesNames.Count);
        // Debug.Log("init node " + this + " is decision " + IsDecision());

        outcomes.Clear();

        for (int i = 0; i < outcomesNames.Count; i++) {
            var outcome  = ScriptableObject.CreateInstance<Outcome>();
            outcome.answer = outcomesNames[i];
            outcome.nextNode = outcomesNodes[i];
            // Debug.Log("init node " + this + " outcome " + outcomesNames[i] + " with next node " + outcomesNodes[i]);
            outcome.nextNode.Init();
            outcomes.Add(outcome);
        }
    }

    public Node(List<Outcome> outcomes) {
        this.outcomes = outcomes;

        foreach (var outcome in outcomes) {
            outcomesNames.Add(outcome.answer);
            outcomesNodes.Add(outcome.nextNode);
        }
    }

    public bool IsDecision() {
        return outcomeDecisionHandler.GetType().IsSubclassOf(typeof(A_OutcomeDecisionHandlerUser));
    }
}
