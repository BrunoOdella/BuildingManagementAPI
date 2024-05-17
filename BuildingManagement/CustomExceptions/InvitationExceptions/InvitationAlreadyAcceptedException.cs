using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.InvitationExceptions
{
    public class InvitationAlreadyAcceptedException : CustomException
    {
        public InvitationAlreadyAcceptedException() : base("Invitation has already been accepted.")
        {
        }
    }
}
