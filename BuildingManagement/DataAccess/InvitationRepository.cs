using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            return invitation;
        }

        public bool DeleteInvitation(int invitationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _context.Invitations.ToList();
        }
    }
}
