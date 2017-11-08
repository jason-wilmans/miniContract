using System;

namespace MiniContract.Exceptions
{
    public class PostConditionViolatedException : Exception
    {
        private Func<bool> _condition;

        public PostConditionViolatedException()
        {
        }

        public PostConditionViolatedException(string message)
            : base(message)
        {
        }

        public PostConditionViolatedException(Func<bool> condition)
        {
            _condition = condition;
        }

        public PostConditionViolatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}