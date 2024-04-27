﻿using Domain;
using IDataAccess;
using LogicInterface.Interfaces;

namespace BusinessLogic.Logics
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        public InvitationLogic(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public Invitation AcceptInvitation(Guid invitationId, string password)
        {
            var invitation = _invitationRepository.GetInvitationById(invitationId);
            if (invitation == null || invitation.Status == "Aceptada")
            {
                throw new InvalidOperationException("Invitation cannot be accepted.");
            }

            invitation.Status = "Aceptada";
            _invitationRepository.UpdateInvitation(invitation);

            //Aca se crea el usuario nuevo 

            return invitation;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            return _invitationRepository.CreateInvitation(invitation);
        }

        public bool DeleteInvitation(Guid invitationId)
        {
            var invitation = _invitationRepository.GetInvitationById(invitationId);
            if (invitation == null)
            {
                return false; 
            }

            if (invitation.Status == "Aceptada")
            {
                throw new InvalidOperationException("No se puede eliminar una invitación aceptada.");
            }

            _invitationRepository.DeleteInvitation(invitationId);
            return true;
        }



        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _invitationRepository.GetAllInvitations();
        }
    }
}
