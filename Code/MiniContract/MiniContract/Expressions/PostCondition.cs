// <copyright file="PostCondition.cs" year="2017" owner="Fritz Oscar S. Berngruber & Jason Wilmans" email="fw.project@gmx.de">
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// </copyright>

using System;
using MiniContract.Exceptions;

namespace MiniContract.Expressions
{
    public static class PostCondition
    {
        public static void Is(Func<bool> condition, string message = null)
        {
#if DEBUG
            if (!condition())
            {
                if (message != null)
                    throw new PostConditionViolatedException(message);

                throw new PostConditionViolatedException(condition);
            }
#endif
        }

        public static void NotNull(object reference)
        {
#if DEBUG
            if (reference == null)
                throw new PostConditionViolatedException("Object reference was null.");
#endif
        }
    }
}