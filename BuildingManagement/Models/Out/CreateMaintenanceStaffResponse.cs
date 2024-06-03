using Domain;

namespace Models.Out;

public class CreateMaintenanceStaffResponse
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public CreateMaintenanceStaffResponse(MaintenanceStaff staff)
    {
        this.ID = staff.ID;
        this.Name = staff.Name;
        this.LastName = staff.LastName;
        this.Email = staff.Email;
        this.Password = staff.Password;
    }

}