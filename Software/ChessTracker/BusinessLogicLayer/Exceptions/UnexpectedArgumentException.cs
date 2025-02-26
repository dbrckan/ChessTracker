using System;

namespace BusinessLogicLayer.Exceptions
{
    public class UnexpectedArgumentException : Exception
    {
        public UnexpectedArgumentException(string message) : base(message)
        {
            
        }
    }
}