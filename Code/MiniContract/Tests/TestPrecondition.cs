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
        public void PreIs_ConditionFulfilled_DoesNotThrowException()
        {
            // Arrange & Act
            Precondition.Is(() => true);
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void PreIs_ConditionNotFulfilled_DoesThrowException()
        {
            // Arrange & Act
            Precondition.Is(() => false);
        }

        [TestMethod]
        public void PreIs_ConditionNotFulfilledWithMessage_DoesThrowException()
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
        public void PreNotNull_ObjectNotNull_DoesNotThrowException()
        {
            // Arrange & Act
            Precondition.NotNullOrDefault(new object());
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void PreNotNull_ObjectNull_DoesThrowException()
        {
            // Arrange
            object test = null;

            // Act
            Precondition.NotNullOrDefault(test);
        }

    }
}