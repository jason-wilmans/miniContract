using System;
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
    }
}