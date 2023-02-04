using System;
using Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler;
using UnityEngine;

namespace Assets.Scripts.GraphSystem.Model.OutcomeByUserHandler {
    [CreateAssetMenu(menuName = "Textadventure/OutcomeUser")]
    public class OutcomeByUserHandler : A_OutcomeDecisionHandlerUser {
        Action<Outcome> outcomeUpdate;

        public OutcomeByUserHandler(Node node, Action<Outcome> outcomeUpdate) 
            : base(node) 
        {
            this.outcomeUpdate = outcomeUpdate;
        }

        public override void TakeResult(Outcome outcome)
        {
            outcomeUpdate(outcome);
        }
    }
}
