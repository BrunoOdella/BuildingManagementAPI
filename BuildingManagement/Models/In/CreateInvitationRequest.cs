using Domain;
using System;

namespace Models.In
{
    public class CreateInvitationRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Role { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation
            {
                Email = Email,
                Name = Name,
                ExpirationDate = ExpirationDate,
                Role = Role 
            };
        }
    }
}