using System;
using System.Collections.Generic;
using MiniContract.Exceptions;

namespace MiniContract.Expressions
{
    // ReSharper disable ParameterOnlyUsedForPreconditionCheck.Global
    public static class Precondition
    {
        public static void Is(Func<bool> condition, string message = null)
        {
            if (condition == null)
            {
                throw new InvalidContractException(ExceptionMessages.MissingContract);
            }

            if (!condition())
            {
                if (message != null)
                    throw new PreconditionViolatedException(message);

                throw new PreconditionViolatedException(condition);
            }
        }
        
        public static void NotNullOrDefault<T>(T value)
        {
            if (value == null)
            {
                throw new PreconditionViolatedException(ExceptionMessages.NullValue);
            }

            if (value.Equals(default(T)))
            {
                throw new PreconditionViolatedException(ExceptionMessages.NullOrDefault<T>());
            }
                
        }
        
        public static void NotNull<T>(T value) where T : class
        {
            if (value == null)
            {
                throw new PreconditionViolatedException(ExceptionMessages.NullValue);
            }
        }

        public static void ElementsNotNullOrDefault<T>(IEnumerable<T> elements)
        {
            if (elements == null)
                throw new PreconditionViolatedException(ExceptionMessages.EmptyCollection);

            int nullOrDefaultCount = 0;
            foreach (T element in elements)
            {
                if (element == null || element.Equals(default(T)))
                {
                    nullOrDefaultCount++;
                }
            }

            if (nullOrDefaultCount > 0)
            {
                throw new PreconditionViolatedException(ExceptionMessages.ElementNullOrDefault(elements, nullOrDefaultCount));
            }
        }
    }
}