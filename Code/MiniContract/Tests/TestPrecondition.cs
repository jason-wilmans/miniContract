using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniContract.Expressions;
using MiniContract.Exceptions;
using NFluent;

// ReSharper disable ExpressionIsAlwaysNull
namespace Tests
{
    [TestClass]
    public class TestPrecondition
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidContractException))]
        public void Is_InvalidConditionNoMessage_DoesThrowException()
        {
            // Arrange & Act
            Precondition.Is(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidContractException))]
        public void Is_InvalidConditionWithMessage_DoesThrowException()
        {
            // Arrange & Act
            Precondition.Is(null, "I am a test :)");
        }

        [TestMethod]
        public void Is_ConditionFulfilled_DoesNotThrowException()
        {
            // Arrange & Act
            Precondition.Is(() => true);
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void Is_ConditionNotFulfilled_DoesThrowException()
        {
            // Arrange & Act
            Precondition.Is(() => false);
        }

        [TestMethod]
        public void Is_ConditionNotFulfilledWithMessage_DoesThrowException()
        {
            // Arrange
            bool exceptionThrown = false;
            PreconditionViolatedException exception = null;

            // Act
            try
            {
                Precondition.Is(() => false, "Test message");
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
                exception = ex as PreconditionViolatedException;
            }

            // Assert
            Check.That(exceptionThrown).IsTrue();
            Check.That(exception).IsInstanceOf<PreconditionViolatedException>();
            Check.That(exception.Message).Equals("Test message");
        }

        [TestMethod]
        public void NotNullOrDefault_ObjectNotNull_DoesNotThrowException()
        {
            // Arrange & Act
            Precondition.NotNullOrDefault(new object());
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void NotNullOrDefault_ObjectNull_DoesThrowException()
        {
            // Arrange
            object test = null;

            // Act
            Precondition.NotNullOrDefault(test);
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void NotNull_ObjectNull_DoesThrowException()
        {
            // Arrange
            object test = null;

            // Act
            Precondition.NotNull(test);
        }

        [TestMethod]
        public void NotNull_ObjectNotNull_DoesNotThrowException()
        {
            // Arrange
            object test = new object();

            // Act
            Precondition.NotNull(test);
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void ElementsNotNullOrDefault_CollectionNull_DoesThrowException()
        {
            // Arrange
            IEnumerable<object> elements = null;

            // Act
            Precondition.ElementsNotNullOrDefault(elements);
        }

        [TestMethod]
        public void ElementsNotNullOrDefault_EmptyReferenceCollection_DoesNotThrowException()
        {
            // Arrange
            IEnumerable<object> elements = new List<object>();

            // Act
            Precondition.ElementsNotNullOrDefault(elements);
        }

        [TestMethod]
        public void ElementsNotNullOrDefault_EmptyValueTypeCollection_DoesNotThrowException()
        {
            // Arrange
            IEnumerable<int> elements = new List<int>();

            // Act
            Precondition.ElementsNotNullOrDefault(elements);
        }

        [TestMethod]
        public void ElementsNotNullOrDefault_OneNullElement_DoesThrowException()
        {
            // Arrange
            IEnumerable<object> elements = new List<object>
            {
                new object(),
                null
            };

            PreconditionViolatedException exception = null;

            // Act
            try
            {
                Precondition.ElementsNotNullOrDefault(elements);
            }
            catch (PreconditionViolatedException e)
            {
                exception = e;
            }

            // Assert
            Check.That(exception).IsNotNull();
            Check.That(exception.Message).Contains("1 element");
        }

        [TestMethod]
        public void ElementsNotNullOrDefault_TwoDefaultValues_DoesThrowException()
        {
            // Arrange
            IEnumerable<int> elements = new List<int>
            {
                0,
                42,
                72,
                6,
                0
            };

            PreconditionViolatedException exception = null;

            // Act
            try
            {
                Precondition.ElementsNotNullOrDefault(elements);
            }
            catch (PreconditionViolatedException e)
            {
                exception = e;
            }

            // Assert
            Check.That(exception).IsNotNull();
            Check.That(exception.Message).Contains("2 elements");
        }
    }
}