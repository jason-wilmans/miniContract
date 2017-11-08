using System;

namespace MiniContract.Exceptions
{
    public class PreconditionViolatedException : Exception
    {
        private Func<bool> _precondition;

        public PreconditionViolatedException()
        {
        }

        public PreconditionViolatedException(string message)
            : base(message)
        {
        }

        public PreconditionViolatedException(Func<bool> precondition)
        {
            _precondition = precondition;
        }

        public PreconditionViolatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}