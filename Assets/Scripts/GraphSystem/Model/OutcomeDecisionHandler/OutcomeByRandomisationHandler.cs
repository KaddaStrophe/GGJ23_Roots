using UnityEngine;

namespace Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler
{
    [CreateAssetMenu(menuName = "Textadventure/OutcomeRandomisation")]
    public class OutcomeByRandomisationHandler : A_OutcomeDecisionHandlerAuto
    {
        private static readonly System.Random rnd = new();

        public OutcomeByRandomisationHandler(Node node) : base (node) {}

        public override Outcome RetrieveResult()
        {
            return node.outcomes[rnd.Next(node.outcomes.Count)];
        }
    }
}
