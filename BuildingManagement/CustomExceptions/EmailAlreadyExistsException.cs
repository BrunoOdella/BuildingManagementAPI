using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class EmailAlreadyExistsException : CustomException
    {
        public EmailAlreadyExistsException() : base("An account with the same email already exists in the system.")
        {
        }
    }
}
