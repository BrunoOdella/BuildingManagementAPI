using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExceptions.InvitationExceptions
{
    public class InvitationNotFoundException : CustomException
    {
        public InvitationNotFoundException() : base("Invitation does not exist.")
        {
        }
    }
}
