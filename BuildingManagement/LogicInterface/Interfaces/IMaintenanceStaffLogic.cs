﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface.Interfaces
{
    public interface IMaintenanceStaffLogic
    {
        MaintenanceStaff AddMaintenanceStaff(string managerId, MaintenanceStaff maintenanceStaff);
    }
}