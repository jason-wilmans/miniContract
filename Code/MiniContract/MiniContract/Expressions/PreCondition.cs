// <copyright file="PreCondition.cs" year="2017" owner="Fritz Oscar S. Berngruber & Jason Wilmans" email="fw.project@gmx.de">
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MiniContract.Exceptions;

namespace MiniContract.Expressions
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class PreCondition
    {
        public static void Is(Func<bool> condition, string message = null)
        {
#if DEBUG
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            if (!condition())
            {
                if (message != null)
                    throw new PreconditionViolatedException(message);

                throw new PreconditionViolatedException(condition);
            }
#endif
        }

        public static void Is(Func<bool> condition, object lockObject, string message = null)
        {
#if DEBUG
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
#endif
        }

        public static void NotNullOrDefault<T>(T value)
        {
#if DEBUG
            if (value == null)
                throw new PreconditionViolatedException("Object value was null.");

            if (value.Equals(default(T)))
                throw new PreconditionViolatedException($"Object value was default({nameof(T)}).");
#endif
        }

        public static void NotNull(object value)
        {
#if DEBUG
            if (value == null)
                throw new PreconditionViolatedException("Object value was null.");
#endif
        }

        public static void ElementsNotNullOrDefault<T>(IEnumerable<T> elements)
        {
#if DEBUG
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
#endif
        }
    }
}