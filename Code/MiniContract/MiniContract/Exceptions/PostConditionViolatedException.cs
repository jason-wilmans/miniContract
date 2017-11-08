// <copyright file="PostConditionviolatedException.cs" year="2017" owner="Fritz Oscar S. Berngruber & Jason Wilmans" email="fw.project@gmx.de">
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// </copyright>

using System;

namespace MiniContract.Exceptions
{
    public class PostConditionViolatedException : Exception
    {
        private Func<bool> _condition;

        public PostConditionViolatedException()
        {
        }

        public PostConditionViolatedException(string message)
            : base(message)
        {
        }

        public PostConditionViolatedException(Func<bool> condition)
        {
            _condition = condition;
        }

        public PostConditionViolatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}