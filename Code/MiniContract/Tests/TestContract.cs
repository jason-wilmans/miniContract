// <copyright file="TestContract.cs" year="2017" owner="Fritz Oscar S. Berngruber & Jason Wilmans" email="fw.project@gmx.de">
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniContract.Expressions;
using MiniContract.Exceptions;
using NFluent;

// ReSharper disable ExpressionIsAlwaysNull

namespace Tests
{
    [TestClass]
    public class TestContract
    {
        [TestMethod]
        public void PreIs_ConditionFulfilled_DoesNotThrowException()
        {
            // Arrange & Act
            PreCondition.Is(() => true);
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void PreIs_ConditionNotFulfilled_DoesThrowException()
        {
            // Arrange & Act
            PreCondition.Is(() => false);
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
                PreCondition.Is(() => false, "Test message");
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
            PreCondition.NotNullOrDefault(new object());
        }

        [TestMethod]
        [ExpectedException(typeof(PreconditionViolatedException))]
        public void PreNotNull_ObjectNull_DoesThrowException()
        {
            // Arrange
            object test = null;

            // Act
            PreCondition.NotNullOrDefault(test);
        }

        [TestMethod]
        public void PostIs_ConditionFulfilled_DoesNotThrowException()
        {
            // Arrange & Act
            PostCondition.Is(() => true);
        }

        [TestMethod]
        [ExpectedException(typeof(PostConditionViolatedException))]
        public void PostIs_ConditionNotFulfilled_DoesThrowException()
        {
            // Arrange & Act
            PostCondition.Is(() => false);
        }

        [TestMethod]
        public void PostIs_ConditionNotFulfilledWithMessage_DoesThrowException()
        {
            // Arrange
            bool exceptionThrown = false;
            PostConditionViolatedException exception = null;

            // Act
            try
            {
                PostCondition.Is(() => false, "Test message");
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
        public void PostNotNull_ObjectNotNull_DoesNotThrowException()
        {
            // Arrange & Act
            PostCondition.NotNull(new object());
        }

        [TestMethod]
        [ExpectedException(typeof(PostConditionViolatedException))]
        public void PostNotNull_ObjectNull_DoesThrowException()
        {
            // Arrange & Act
            PostCondition.NotNull(null);
        }
    }
}