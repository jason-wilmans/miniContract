using System;
using MiniContract.Exceptions;
namespace MiniContract.Expressions
{
    // ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
    public static class Postcondition
    {
        public static void Is(Func<bool> condition, string message = null)
        {
            if (condition == null)
            {
                throw new InvalidContractException(ExceptionMessages.MissingContract);
            }

            if (condition()) return;

            if (message != null)
            {
                throw new PostConditionViolatedException(message);
            }

            throw new PostConditionViolatedException(condition);
        }
        
        public static void NotNull(object reference)
        {
            if (reference == null)
                throw new PostConditionViolatedException(ExceptionMessages.NullValue);
        }
    }
}