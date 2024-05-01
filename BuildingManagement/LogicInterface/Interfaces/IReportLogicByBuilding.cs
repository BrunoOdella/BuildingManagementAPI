using Domain;

namespace LogicInterface.Interfaces;

public interface IReportLogicByBuilding
{
    Report RequestByBuilding(Guid managerId);
    Report RequestByBuilding(Guid managerID, Guid guid);
}