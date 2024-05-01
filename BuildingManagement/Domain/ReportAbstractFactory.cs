namespace Domain;

public abstract class ReportAbstractFactory
{
    public abstract Report_RequestByBuilding createReportRequestByBuilding();
    public abstract Report_RequestByMaintenanceStaff createReportRequestByMaintenanceStaff();
}