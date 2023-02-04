namespace Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler
{
    public abstract class A_OutcomeDecisionHandlerAuto : A_OutcomeDecisionHandler
    {
        public A_OutcomeDecisionHandlerAuto(Node node) : base(node) {}

        public abstract Outcome RetrieveResult();
    }
}
