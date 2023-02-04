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

        private readonly List<IObserver<OutcomeNotification>> observers;

        public A_OutcomeDecisionHandler OutcomeUpdate(Outcome outcome)
        {
            Node oldNode = graph.currentNode;

            // handle model update

            if (!oldNode.outcomes.Contains(outcome))
            {
                throw new OutcomeNotInNodeError(outcome.text, oldNode.text);
            }

            if (!oldNode.selectedOutcomes.Contains(outcome))
            {
                oldNode.selectedOutcomes.Add(outcome);
            }

            graph.currentNode = outcome.nextNode;


            // notify observers

            foreach (var observer in observers)
                observer.OnNext(new OutcomeNotification(oldNode, outcome));


            // decide how to proceed with outcome evaluation

            A_OutcomeDecisionHandler handler = graph.currentNode.outcomeDecisionHandler;

            if (handler is A_OutcomeDecisionHandlerAuto)
                return OutcomeUpdate(((A_OutcomeDecisionHandlerAuto)handler).RetrieveResult());
            else
                return (OutcomeByUserHandler)handler;

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
