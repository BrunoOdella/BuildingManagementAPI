using Domain;

namespace LogicInterface.Interfaces;

public interface IReportLogicByMaintenanceStaff
{
    Report RequestByMaintenanceStaff(Guid managerId);
    Report RequestByMaintenanceStaff(Guid managerID, Guid guid);
}