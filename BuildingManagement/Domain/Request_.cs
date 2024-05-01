
namespace Domain;

public class Request_
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; } = Status.Pending;
    public int? Category { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public float TotalCost { get; set; }
    public MaintenanceStaff MaintenanceStaff { get; set; }
}