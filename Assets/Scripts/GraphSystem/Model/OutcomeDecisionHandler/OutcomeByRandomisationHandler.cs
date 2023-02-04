using UnityEngine;

namespace Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler
{
    [CreateAssetMenu(menuName = "Textadventure/OutcomeRandomisation")]
    public class OutcomeByRandomisationHandler : A_OutcomeDecisionHandlerAuto
    {
        static readonly System.Random rnd = new();

        public override Outcome RetrieveResult(Node node)
        {
            return node.outcomes[rnd.Next(node.outcomes.Count - 1)];
        }
    }
}
