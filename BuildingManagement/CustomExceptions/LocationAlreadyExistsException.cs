using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class LocationAlreadyExistsException : CustomException
    {
        public LocationAlreadyExistsException()
            : base("A building with the same location already exists.")
        {
        }
    }
}
