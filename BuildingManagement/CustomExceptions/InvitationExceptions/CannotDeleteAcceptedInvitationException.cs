using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.InvitationExceptions
{
    public class CannotDeleteAcceptedInvitationException : CustomException
    {
        public CannotDeleteAcceptedInvitationException() : base("Cannot delete an accepted invitation.")
        {
        }
    }
}
