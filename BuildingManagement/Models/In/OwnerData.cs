using Domain;

namespace Models.In
{
    public class OwnerData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Owner ToEntity()
        {
            return new Owner
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email
            };
        }
    }

}
