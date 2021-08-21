using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer.Exceptions
{
    public class ValidPositionException : Exception
    {
        public ValidPositionException()
        {
        }

        public ValidPositionException(string message)
            : base(message)
        {
        }

        public ValidPositionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
