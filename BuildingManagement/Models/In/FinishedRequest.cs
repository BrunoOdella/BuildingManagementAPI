using Domain;

namespace Models.In;

public class FinishedRequest
{
    public Status Status { get; set; } = Status.Finished;
    public float TotalCost { get; set; }
    public DateTime EndTime { get; set; }
}