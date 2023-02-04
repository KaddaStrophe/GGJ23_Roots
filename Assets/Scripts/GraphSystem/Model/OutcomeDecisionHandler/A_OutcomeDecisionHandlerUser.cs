namespace Assets.Scripts.GraphSystem.Model.OutcomeDecisionHandler 
{
    public abstract class A_OutcomeDecisionHandlerUser : A_OutcomeDecisionHandler 
    {
        public A_OutcomeDecisionHandlerUser(Node node) : base(node) {}

        public abstract void TakeResult(Outcome outcome);
    }
}
