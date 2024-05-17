using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
