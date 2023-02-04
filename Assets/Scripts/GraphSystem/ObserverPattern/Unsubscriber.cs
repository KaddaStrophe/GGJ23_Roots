using System;
using System.Collections.Generic;

namespace Assets.Scripts.GraphSystem
{
    internal class Unsubscriber<OutcomeNotification> : IDisposable
    {
        private List<IObserver<OutcomeNotification>> _observers;
        private IObserver<OutcomeNotification> _observer;

        public Unsubscriber(List<IObserver<OutcomeNotification>> observers, IObserver<OutcomeNotification> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}