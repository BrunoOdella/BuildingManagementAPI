using Domain;
using IDataAccess;

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
    }
}
