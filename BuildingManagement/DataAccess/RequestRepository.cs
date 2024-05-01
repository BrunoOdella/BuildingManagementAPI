using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

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
            return request;
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId)
        {
            var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();
            var requests = new List<Request_>();

            foreach (var building in buildings)
            {
                foreach (var apartment in building.Apartments)
                {
                    requests.AddRange(apartment.Requests);
                }
            }

            return requests;
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId, int category)
        {
            var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();
            var requests = new List<Request_>();

            foreach (var building in buildings)
            {
                foreach (var apartment in building.Apartments)
                {
                    foreach (var request in apartment.Requests)
                    {
                        if (request.Category == category)
                            requests.Add(request);
                    }
                    
                }
            }

            return requests;
        }

        //no usada
        /*
        public Request_ ActivateRequest(Guid managerId, Guid requestId, DateTime startTime)
        {
            throw new NotImplementedException();
        }
        */

        //no usada
        /*
        public Request_ TerminateRequest(Guid managerId, Guid id, DateTime endTime, float totalCost)
        {
            throw new NotImplementedException();
        }
        */

        //no usada
        /*
        public Request_ AsignMaintenancePerson(Guid managerId, Guid requestGuid, Guid maintenancePersonId)
        {
            throw new NotImplementedException();
        }
        */

        public Request_ GetRequest(Guid managerId, Guid requestId)
        {
            return _context.Requests.FirstOrDefault(i => i.Id.Equals(requestId));
        }

        public void Update(Request_ actualRequest)
        {
            var request = _context.Requests.FirstOrDefault(i => i.Id.Equals(actualRequest.Id));
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
    }
}