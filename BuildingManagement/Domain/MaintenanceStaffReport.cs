namespace Domain;

public class MaintenanceStaffReport
{
    public string StaffName { get; set; }
    public int PendingRequests { get; set; } = 0;
    public int ActiveRequests { get; set; } = 0;
    public int CompletedRequests { get; set; } = 0;
    public double AverageCompletionTimeInHours { get; set; } = 0;
}