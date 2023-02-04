using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GraphSystem.Errors {
    class NodeContainsDecisionError : Exception {

        public NodeContainsDecisionError(string nodeName)
            : base(string.Format(
                "Node '{0}' contains a decision!",
                nodeName)
              ) { }

    }
}
