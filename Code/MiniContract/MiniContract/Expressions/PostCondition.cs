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

        public static T Is<T>(T value, Func<bool> condition, string message = null)
        {
            if (condition == null)
            {
                throw new InvalidContractException(ExceptionMessages.MissingContract);
            }

            if (condition()) return value;

            if (message != null)
            {
                throw new PostConditionViolatedException(message);
            }

            throw new PostConditionViolatedException(condition);
        }
        
        public static T NotNull<T>(T reference) where T : class 
        {
            if (reference == null)
                throw new PostConditionViolatedException(ExceptionMessages.NullValue);

            return reference;
        }
    }
}