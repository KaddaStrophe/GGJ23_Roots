using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GraphSystem.Errors {
    public class CSVRowHasWrongLength : Exception {

        public CSVRowHasWrongLength(int expectedLength, int actualLength)
            : base(string.Format(
                "CSV row has wrong length: Expected '{0}' but got '{1}'",
                expectedLength,
                actualLength)
              ) { }

    }
}
