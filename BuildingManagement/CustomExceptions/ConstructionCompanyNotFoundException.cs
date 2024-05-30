using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class ConstructionCompanyNotFoundException : CustomException
    {
        public ConstructionCompanyNotFoundException()
            : base("You should create your construction company first.") { }
    }
}
