using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.ConstructionCompanyExceptions
{
    public class AdminAlreadyHasCompanyException : CustomException
    {
        public AdminAlreadyHasCompanyException() : base("This admin already has a construction company.")
        {
        }
    }
}
