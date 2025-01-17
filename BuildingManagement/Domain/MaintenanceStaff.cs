﻿namespace Domain;

public class MaintenanceStaff
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Request_> Requests { get; set; } = new List<Request_>();
}