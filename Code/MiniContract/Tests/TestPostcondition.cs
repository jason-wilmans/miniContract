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
        public void IsWithValue_InvalidConditionNoMessage_DoesThrowException()
        {
            // Arrange
            object original = new object();
            object returnedValue = null;

            InvalidContractException exception = null;

            // Act
            try
            {
                returnedValue = Postcondition.Is(original, null);
            }
            catch (InvalidContractException e)
            {
                exception = e;
            }
            
            // Assert
            Check.That(exception).IsNotNull();
            Check.That(returnedValue).IsNull();
        }

        [TestMethod]
        public void IsWithValue_InvalidConditionWithMessage_DoesThrowException()
        {
            // Arrange
            object original = new object();
            object returnedValue = null;

            InvalidContractException exception = null;

            // Act
            try
            {
                returnedValue = Postcondition.Is(original, null, "I'm gonna blow up :(.");
            }
            catch (InvalidContractException e)
            {
                exception = e;
            }

            // Assert
            Check.That(exception).IsNotNull();
            Check.That(returnedValue).IsNull();
        }

        [TestMethod]
        public void IsWithValue_ConditionFulfilled_DoesNotThrowException()
        {
            // Arrange
            object original = new object();

            // Act
            var returnedValue = Postcondition.Is(original, () => true);

            // Arrange
            Check.That(returnedValue).IsSameReferenceAs(original);
        }

        [TestMethod]
        public void IsWithValue_ConditionFulfilledAndMessage_DoesNotThrowException()
        {
            // Arrange
            object original = new object();

            // Act
            var returnedValue = Postcondition.Is(original, () => true, "I will survive :).");

            // Arrange
            Check.That(returnedValue).IsSameReferenceAs(original);
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
            // Arrange
            var reference = new object();

            // Act
            var returnedValue = Postcondition.NotNull(reference);

            // Assert
            Check.That(returnedValue).IsSameReferenceAs(reference);
        }

        [TestMethod]
        public void NotNull_ObjectNull_DoesThrowException()
        {
            // Arrange
            object reference = null;
            object returnedValue = null;

            PostConditionViolatedException exception = null;

            // Act
            try
            {
                returnedValue = Postcondition.NotNull(reference);
            }
            catch (PostConditionViolatedException e)
            {
                exception = e;
            }

            // Assert
            Check.That(exception).IsNotNull();
            Check.That(returnedValue).IsNull();
        }
    }
}
