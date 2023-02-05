using System;

namespace Assets.Scripts.GraphSystem.Errors {
    public class UserOutcomeNotWaitedForError : Exception {

        public UserOutcomeNotWaitedForError(string outcomeName)
            : base(string.Format(
                "User-outcome '{0}' not waited for in any node!",
                outcomeName)
              ) { }

    }
}
