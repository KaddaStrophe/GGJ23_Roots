using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GraphSystem.Errors
{
    public class OutcomeNotInNodeError : Exception {
    
        public OutcomeNotInNodeError(string outcomeName, string nodeName)
            : base(string.Format(
                "Outcome '{0}' not in node '{1}'!", 
                outcomeName, 
                nodeName)
              )
        {}
    
    }
}
