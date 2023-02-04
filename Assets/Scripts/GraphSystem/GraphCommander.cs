using Assets.Scripts.GraphSystem.Errors;
using Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler;
using UnityEngine;

namespace Assets.Scripts.GraphSystem
{
    public class GraphCommander : MonoBehaviour
    {
        [SerializeField]
        Graph graph;

        public Node ProvideStart() {
            graph.Init();
            return graph.startNode;
        }

        public Node OutcomeUpdate(Outcome outcome) {
            var oldNode = graph.currentNode;

            // handle model update

            if (!oldNode.outcomes.Contains(outcome)) {
                throw new OutcomeNotInNodeError(outcome.content, oldNode.content);
            }

            if (!oldNode.selectedOutcomes.Contains(outcome)) {
                oldNode.selectedOutcomes.Add(outcome);
            }

            graph.currentNode = outcome.nextNode;


            // let caller handle outcome evaluation

            return graph.currentNode;
        }

        public Node OutcomeUpdateNoDecision() {
            if (graph.currentNode.outcomeDecisionHandler is A_OutcomeDecisionHandlerAuto autoHandler) {
                // Debug.Log("OutcomeUpdate, old node: " + graph.currentNode + ", handler: " + autoHandler + ", possible outcomes A: " + graph.currentNode.outcomes.Count + ", possible outcomes B: " + graph.currentNode.outcomesNodes.Count);
                return OutcomeUpdate(autoHandler.RetrieveResult(graph.currentNode));
            } else {
                throw new NodeContainsDecisionError(graph.currentNode.content);
            }
        }
    }
}
