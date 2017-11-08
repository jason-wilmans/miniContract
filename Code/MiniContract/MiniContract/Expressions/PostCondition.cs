using System;
using MiniContract.Exceptions;
namespace MiniContract.Expressions
{
    // ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
    public static class Postcondition
    {
        public static void Is(bool condition, string message = null)
        {
            if (condition) return;

            if (message != null)
            {
                throw new PostConditionViolatedException(message);
            }

            throw new PostConditionViolatedException(ExceptionMessages.ConditionNotSatisfied);
        }

        public static T Is<T>(T value, bool condition, string message = null)
        {
            if (condition) return value;

            if (message != null)
            {
                throw new PostConditionViolatedException(message);
            }

            throw new PostConditionViolatedException(ExceptionMessages.ConditionNotSatisfied);
        }
        
        public static T NotNull<T>(T reference) where T : class 
        {
            if (reference == null)
                throw new PostConditionViolatedException(ExceptionMessages.NullValue);

            return reference;
        }
    }
}