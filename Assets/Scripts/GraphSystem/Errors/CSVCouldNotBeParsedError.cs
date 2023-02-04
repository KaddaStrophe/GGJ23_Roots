using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.GraphSystem.Errors {
    public class CSVPropertyCouldNotBeParsedError : Exception {

        public CSVPropertyCouldNotBeParsedError(string propertyThatCouldNotBeParsed, string valueToParse, string expectedType)
            : base(string.Format(
                "Property '{0}' of value '{1}' failed to parse as {2}.",
                propertyThatCouldNotBeParsed,
                valueToParse,
                expectedType)
              ) 
        { }

    }
}
