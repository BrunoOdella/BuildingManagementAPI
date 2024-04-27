using Domain;

namespace Models.Out
{
    public class AdminResponse
    {
        public Guid AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AdminResponse(Admin admin)
        {
            this.AdminId = admin.AdminID;
            this.FirstName = admin.FirstName;
            this.LastName = admin.LastName;
            this.Email = admin.Email;
        }

        public override bool Equals(object obj)
        {
            return obj is AdminResponse response &&
                   AdminId == response.AdminId &&
                   FirstName == response.FirstName &&
                   LastName == response.LastName &&
                   Email == response.Email;
        }
    }
}
