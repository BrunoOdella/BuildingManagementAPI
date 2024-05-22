using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.ConstructionCompanyExceptions
{
    public class ConstructionCompanyAlreadyExistsException : CustomException
    {
        public ConstructionCompanyAlreadyExistsException() : base("A construction company with the same name already exists.")
        {
        }
    }
}
