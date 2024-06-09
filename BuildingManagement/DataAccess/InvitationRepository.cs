using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class InvitationRepository : IInvitationRepository
    {

        private readonly BuildingManagementDbContext _context;

        public InvitationRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            _context.Invitations.Add(invitation);
            _context.SaveChanges();
            return invitation;
        }

        public bool DeleteInvitation(Guid invitationId)
        {
            var invitation = _context.Invitations.Find(invitationId);
            if (invitation == null)
            {
                return false;
            }

            _context.Invitations.Remove(invitation);
            _context.SaveChanges();
            return true;
        }


        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _context.Invitations.ToList();
        }

        public Invitation GetInvitationById(Guid invitationId)
        {
            return _context.Invitations.FirstOrDefault(i => i.InvitationId == invitationId);
        }

        public void UpdateInvitation(Invitation updatedInvitation)
        {
            var invitation = _context.Invitations.FirstOrDefault(i => i.InvitationId == updatedInvitation.InvitationId);
            if (invitation != null)
            {
                invitation.Status = updatedInvitation.Status;
                _context.Entry(invitation).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Invitation not found.");
            }
        }
        public bool EmailExistsInInvitations(string email)
        {
            return _context.Invitations.Any(i => i.Email == email);
        }
    }
}
