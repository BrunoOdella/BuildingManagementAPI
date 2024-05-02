﻿using Domain;

namespace IDataAccess;

public interface IMaintenanceStaffRepository
{
    IEnumerable<MaintenanceStaff> GetAll(Guid managerId);
    MaintenanceStaff GetMaintenanceStaff(Guid managerId, Guid maintenancePersonId);
    Guid GetMaintenanceStaff(Guid maintenancePersonId);
    void Update(MaintenanceStaff actualMaintenanceStaff);
}