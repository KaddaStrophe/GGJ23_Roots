using System.Collections.Generic;
using Assets.Scripts.GraphSystem.Errors;
using Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler;
using UnityEngine;

namespace Assets.Scripts.GraphSystem
{
    public class GraphCommander : MonoBehaviour
    {
        [SerializeField]
        Graph graph;

        List<Node> nodesWaitingForOutcome = new();

        public Node ProvideStart() {
            graph.Init();
            nodesWaitingForOutcome.Add(graph.startNode);
            return graph.startNode;
        }

        public Node OutcomeUpdate(Outcome outcome) {

            // handle model update

            Node oldNode = null;

            foreach (var waitingNode in nodesWaitingForOutcome) {

                // Debug.Log("node: " + waitingNode + " no outcomes: " + waitingNode.outcomes.Count);

                if (waitingNode.outcomes.Contains(outcome)) {
                    oldNode = waitingNode;
                    break;
                }
            }

            if (oldNode == null) {
                throw new UserOutcomeNotWaitedForError(outcome.answer);
            }

            Debug.Log("User-outcome with answer '" + outcome.answer + "' registered.");

            nodesWaitingForOutcome.Remove(oldNode);

            if (!oldNode.selectedOutcomes.Contains(outcome)) {
                oldNode.selectedOutcomes.Add(outcome);
            }

            // caller handles time of next outcome evaluation

            nodesWaitingForOutcome.Add(outcome.nextNode);

            return outcome.nextNode;
        }

        public Node OutcomeUpdateNoDecision() {

            // handle model update

            Node oldNode = null;
            A_OutcomeDecisionHandlerAuto oldHandler = null;

            foreach (var waitingNode in nodesWaitingForOutcome) {

                if (waitingNode.outcomeDecisionHandler is A_OutcomeDecisionHandlerAuto autoHandler) {
                    oldNode = waitingNode;
                    oldHandler = autoHandler;
                    break;

                }
            }

            if (oldNode == null) {
                throw new NoAutoOutcomeWaitedForError();
            }

            // auto nodes retrieve outcome themselves
            var outcome  = oldHandler.RetrieveResult(oldNode);

            // Debug.Log("Auto-outcome with answer '" + outcome.answer + "' registered.");

            nodesWaitingForOutcome.Remove(oldNode);

            if (!oldNode.selectedOutcomes.Contains(outcome)) {
                oldNode.selectedOutcomes.Add(outcome);
            }

            // caller handles time of next outcome evaluation

            nodesWaitingForOutcome.Add(outcome.nextNode);

            return outcome.nextNode;
        }
    }
}
