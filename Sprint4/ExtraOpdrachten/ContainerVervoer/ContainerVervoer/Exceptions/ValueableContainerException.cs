using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer.Exceptions
{
    public class ValueableContainerException : Exception
    {
        public ValueableContainerException()
        {
        }

        public ValueableContainerException(string message)
            : base(message)
        {
        }

        public ValueableContainerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
