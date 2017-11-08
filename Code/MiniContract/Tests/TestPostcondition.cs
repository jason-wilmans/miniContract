using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniContract.Exceptions;
using MiniContract.Expressions;
using NFluent;

namespace Tests
{
    [TestClass]
    public class TestPostcondition
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidContractException))]
        public void Is_InvalidConditionNoMessage_DoesThrowException()
        {
            // Arrange & Act
            Postcondition.Is(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidContractException))]
        public void Is_InvalidConditionWithMessage_DoesThrowException()
        {
            // Arrange & Act
            Postcondition.Is(null, "I am a test :)");
        }

        [TestMethod]
        public void Is_ConditionFulfilled_DoesNotThrowException()
        {
            // Arrange & Act
            Postcondition.Is(() => true);
        }

        [TestMethod]
        [ExpectedException(typeof(PostConditionViolatedException))]
        public void Is_ConditionNotFulfilled_DoesThrowException()
        {
            // Arrange & Act
            Postcondition.Is(() => false);
        }

        [TestMethod]
        public void Is_ConditionNotFulfilledWithMessage_DoesThrowException()
        {
            // Arrange
            bool exceptionThrown = false;
            PostConditionViolatedException exception = null;

            // Act
            try
            {
                Postcondition.Is(() => false, "Test message");
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
                exception = ex as PostConditionViolatedException;
            }

            // Assert
            Check.That(exceptionThrown).IsTrue();
            Check.That(exception).IsInstanceOf<PostConditionViolatedException>();
            Check.That(exception.Message).Equals("Test message");
        }

        [TestMethod]
        public void NotNull_ObjectNotNull_DoesNotThrowException()
        {
            // Arrange & Act
            Postcondition.NotNull(new object());
        }

        [TestMethod]
        [ExpectedException(typeof(PostConditionViolatedException))]
        public void NotNull_ObjectNull_DoesThrowException()
        {
            // Arrange & Act
            Postcondition.NotNull(null);
        }
    }
}
