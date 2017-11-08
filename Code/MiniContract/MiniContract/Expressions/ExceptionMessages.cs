using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiniContract.Expressions
{
    /* jason: Kinda ironic you can't express preconditions in a contract framework.
     * Can't do that, because it would generate an endless recursion. */
    internal static class ExceptionMessages
    {
        public const string NullValue = "Object value must not be null.";

        public const string EmptyCollection = "The checked collection was null itself.";

        public const string ConditionNotSatisfied = "The condition was not satisfied.";

        public static string NullOrDefault<T>()
        {
            string nullOrDefault = DetermineNullOrDefault<T>();
            return string.Format("Object value {0}.", nullOrDefault);
        }

        public static string ElementNullOrDefault<T>(IEnumerable<T> enumerable, int nullOrDefaultCount)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (nullOrDefaultCount < 0) throw new ArgumentException("This must be a programing error, the count cannot be less than 0.", nameof(nullOrDefaultCount));

            string multipleS = nullOrDefaultCount > 1 ? "s" : "";
            string nullOrDefault = DetermineNullOrDefault<T>();
            return string.Format(
                "{0} element{1} in the given enumerable {2}.",
                nullOrDefaultCount,
                multipleS,
                nullOrDefault);
        }

        private static string DetermineNullOrDefault<T>()
        {
            if (typeof(T).GetTypeInfo().IsValueType)
            {
                return string.Format("had the default value of the type '{0}'", nameof(T));
            }

            return "was null";
        }
    }
}
