using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BuildingManagementDbContext _context;

        public RequestRepository(BuildingManagementDbContext context)
        {
            _context = context;
        }

        public Request_ CreateRequest(Request_ request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId)
        {
            List<Request_> requests = _context.Requests
                .Include(r => r.Apartment)
                .ThenInclude(a => a.Building)
                .Where(r => r.Apartment.Building.ManagerId == managerId)
                .ToList();
            return requests;
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId, int category)
        {
            List<Request_> requests = _context.Requests
                .Include(r => r.Apartment)
                .ThenInclude(a => a.Building)
                .Where(r => r.Apartment.Building.ManagerId == managerId && r.CategoryID == category)
                .ToList();
            return requests;
        }

        public Request_ GetRequest(Guid managerId, Guid requestId)
        {
            Request_ request = _context.Requests
                .Include(r => r.Apartment)
                .ThenInclude(a => a.Building)
                .FirstOrDefault(r => r.Id == requestId && r.Apartment.Building.ManagerId == managerId);
            return request;
        }

        public void Update(Request_ actualRequest)
        {
            Request_ request = _context.Requests.FirstOrDefault(i => i.Id.Equals(actualRequest.Id));
            if (request != null)
            {
                request.Status = actualRequest.Status;
                request.StartTime = actualRequest.StartTime;
                request.MaintenanceStaff = actualRequest.MaintenanceStaff;

                if (actualRequest.Status == Status.Finished)
                {
                    request.EndTime = actualRequest.EndTime;
                    request.TotalCost = actualRequest.TotalCost;
                }

                _context.Entry(request).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Request_> GetAllRequestStaff(Guid staffID)
        {
            List<Request_> requests = _context.Requests
                .Include(r => r.MaintenanceStaff)
                .Where(r => r.MaintenanceStaff.ID == staffID)
                .ToList();
            return requests;
        }
    }
}
