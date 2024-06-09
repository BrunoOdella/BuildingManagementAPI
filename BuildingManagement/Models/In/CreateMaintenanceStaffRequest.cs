using Domain;

namespace Models.In
{
    public class CreateMaintenanceStaffRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public MaintenanceStaff ToEntity()
        {
            return new MaintenanceStaff
            {
                Name = this.Name,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password,
            };
        }
    }
}
