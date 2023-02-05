namespace TheRuinsBeneath.Visualization {
    public interface ITween {

        void SetDelay(float animationDelay);
        void GreyOut();
        bool FinishAnimation();
        bool GetIsFinished();
    }
}