using System.Runtime.InteropServices.JavaScript;
using Domain;

namespace Models.In;

public class ActivateRequest
{
    public Status Status { get; set; } = Status.Active;
    public DateTime StartTime { get; set; }
    public Guid MaintenancePersonId { get; set; }

}