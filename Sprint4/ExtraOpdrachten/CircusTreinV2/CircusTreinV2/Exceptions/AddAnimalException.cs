using System;
using System.Collections.Generic;
using System.Text;

namespace CircusTreinV2.Exceptions
{
    public class AddAnimalException : Exception
    {
        public AddAnimalException()
        {
        }

        public AddAnimalException(string message)
            : base(message)
        {
        }

        public AddAnimalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
