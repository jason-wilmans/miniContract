using System;

namespace MiniContract.Exceptions
{
    /// <summary>
    /// This exception is thrown, if a contract is improperly defined.
    /// </summary>
    public class InvalidContractException : Exception
    {
        public Func<bool> Condition { get; }

        public InvalidContractException()
        {
        }

        public InvalidContractException(string message)
            : base(message)
        {
        }

        public InvalidContractException(Func<bool> condition)
        {
            Condition = condition;
        }

        public InvalidContractException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
