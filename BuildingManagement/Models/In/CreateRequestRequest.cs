﻿using Domain;

namespace Models.In;

public class CreateRequestRequest
{
    public string Description { get; set; }
    public int Category { get; set; }
    public DateTime CreationTime { get; set; }
    public Guid ApartmentID { get; set; }
    public Guid MaintenanceStaffID { get; set; }

    public Request_ ToEntity()
    {
        return new Request_()
        {
            Description = Description,
            CategoryID = Category,
            CreationTime = CreationTime,
            Apartment = new Apartment() { ApartmentId = ApartmentID },
            MaintenanceStaff = new MaintenanceStaff() { ID = MaintenanceStaffID },
            MaintenanceStaffId = MaintenanceStaffID,
            ApartmentId = ApartmentID
        };
    }
}