using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GraphSystem
{
    public class OutcomeNotification
    {
        public Node oldNode;
        public Outcome outcome;
        public Node newNode;

        public OutcomeNotification(Node oldNode, Outcome outcome)
        {
            this.oldNode = oldNode;
            this.outcome = outcome;
            this.newNode = outcome.nextNode;
        }

    }
}
