using Domain;

namespace Models.Out;

public class RequestResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public int? Category { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public float TotalCost { get; set; }

    public RequestResponse(Request_ request)
    {
        Id = request.Id;
        Description = request.Description;
        Status = request.Status;
        Category = request.Category;
        CreationTime = request.CreationTime;
        StartTime = request.StartTime;
        EndTime = request.EndTime;
        TotalCost = request.TotalCost;
    }

    public override bool Equals(object obj)
    {
        return obj is RequestResponse response &&
               Id == response.Id &&
               Description == response.Description &&
               Status == response.Status &&
               Category == response.Category &&
               CreationTime == response.CreationTime &&
               StartTime == response.StartTime && 
               EndTime == response.EndTime && 
               TotalCost == response.TotalCost;
    }
}