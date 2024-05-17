using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.InvitationExceptions
{
    public class InvitationExpiredException : CustomException
    {
        public InvitationExpiredException() : base("Invitation has expired.")
        {
        }
    }
}
