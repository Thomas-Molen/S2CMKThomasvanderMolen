using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer.Exceptions
{
    public class AddContainerException : Exception
    {
        public AddContainerException()
        {
        }

        public AddContainerException(string message)
            : base(message)
        {
        }

        public AddContainerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
