using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MiniContract.Exceptions;

namespace MiniContract.Expressions
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class Precondition
    {
        public static void Is(Func<bool> condition, string message = null)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            if (!condition())
            {
                if (message != null)
                    throw new PreconditionViolatedException(message);

                throw new PreconditionViolatedException(condition);
            }
        }

        public static void Is(Func<bool> condition, object lockObject, string message = null)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (lockObject == null) throw new ArgumentNullException(nameof(lockObject));

            lock (lockObject)
            {
                if (!condition())
                {
                    if (message != null)
                        throw new PreconditionViolatedException(message);

                    throw new PreconditionViolatedException(condition);
                }
            }
        }

        public static void NotNullOrDefault<T>(T value)
        {
            if (value == null)
                throw new PreconditionViolatedException("Object value was null.");

            if (value.Equals(default(T)))
                throw new PreconditionViolatedException($"Object value was default({nameof(T)}).");
        }

        public static void NotNull(object value)
        {
            if (value == null)
                throw new PreconditionViolatedException("Object value was null.");
        }

        public static void ElementsNotNullOrDefault<T>(IEnumerable<T> elements)
        {
            if (elements == null) throw new ArgumentNullException(nameof(elements));
            foreach (T element in elements)
            {
                if (element == null)
                    throw new PreconditionViolatedException(
                        $"An element in the given {nameof(IEnumerable<T>)} was null.");

                if (element.Equals(default(T)))
                    throw new PreconditionViolatedException(
                        $"An element in the given {nameof(IEnumerable<T>)} was default.");
            }
        }
    }
}