using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException() : base("Invalid email or password")
        {
        }
    }
}
