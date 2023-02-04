using System;
using UnityEngine;

namespace Assets.Scripts.GraphSystem.Model.A_OutcomeDecisionHandlerUser
{
    [CreateAssetMenu(menuName = "Textadventure/OutcomeUser")]
    public class OutcomeByUserHandler : A_OutcomeDecisionHandler
    {
        private Action<Outcome> outcomeUpdate;

        public OutcomeByUserHandler(Node node, Action<Outcome> outcomeUpdate) 
            : base(node) 
        {
            this.outcomeUpdate = outcomeUpdate;
        }

        public void HandleUserOutcomeDecision(Outcome outcome)
        {
            outcomeUpdate(outcome);
        }
    }
}
