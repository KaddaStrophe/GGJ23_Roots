using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GraphSystem.Errors {
    class NoAutoOutcomeWaitedForError : Exception {

        public NoAutoOutcomeWaitedForError() 
            : base(string.Format("No node waiting for outcome is an auto-node!")) 
        { }

    }
}
