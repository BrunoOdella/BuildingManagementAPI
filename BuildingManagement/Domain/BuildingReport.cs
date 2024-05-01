namespace Domain;

public class BuildingReport
{
    public string BuildingName { get; set; }
    public int PendingRequests { get; set; }
    public int ActiveRequests { get; set; }
    public int CompletedRequests { get; set; }
}