using System;
using System.Collections.Generic;
using Assets.Scripts.GraphSystem.Errors;
using Assets.Scripts.GraphSystem.Model;
using Assets.Scripts.GraphSystem.Model.A_OutcomeDecisionHandlerUser;
using Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler;

namespace Assets.Scripts.GraphSystem
{
    public class GraphCommander : IObservable<OutcomeNotification>
    {
        public Graph graph;

        readonly List<IObserver<OutcomeNotification>> observers;

        public A_OutcomeDecisionHandler OutcomeUpdate(Outcome outcome) {
            var oldNode = graph.currentNode;

            // handle model update

            if (!oldNode.outcomes.Contains(outcome)) {
                throw new OutcomeNotInNodeError(outcome.content, oldNode.content);
            }

            if (!oldNode.selectedOutcomes.Contains(outcome)) {
                oldNode.selectedOutcomes.Add(outcome);
            }

            graph.currentNode = outcome.nextNode;


            // notify observers

            foreach (var observer in observers) {
                observer.OnNext(new OutcomeNotification(oldNode, outcome));
            }


            // decide how to proceed with outcome evaluation

            var handler = graph.currentNode.outcomeDecisionHandler;

            if (handler is A_OutcomeDecisionHandlerAuto auto) {
                return OutcomeUpdate(auto.RetrieveResult());
            }
            else {
                return (OutcomeByUserHandler)handler;
            }

        }

        public IDisposable Subscribe(IObserver<OutcomeNotification> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber<OutcomeNotification>(observers, observer);
        }
    }
}
