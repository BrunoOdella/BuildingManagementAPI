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

            _context.SaveChanges();

            return request;
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId)
        {
            var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();
            List<Apartment> apartments = new List<Apartment>();
            foreach (var buiding in buildings)
            {
                var actualApartments = _context.Apartments.Where(a => a.BuildingId.Equals(buiding.BuildingId)).ToList();
                apartments.AddRange(actualApartments);
            }

            var requests = new List<Request_>();

            foreach (var apartment in apartments)
            {
                requests.AddRange(_context.Requests.Where(r => r.ApartmentId.Equals(apartment.ApartmentId)).ToList());
            }

            return requests;
        }

        public IEnumerable<Request_> GetAllRequest(Guid managerId, int category)
        {
            var buildings = _context.Buildings.Where(b => b.ManagerId.Equals(managerId)).ToList();
            var requestsResponse = new List<Request_>();

            foreach (var building in buildings)
            {
                var apartments = _context.Apartments.Where(a => a.BuildingId.Equals(building.BuildingId)).ToList();
                foreach (var apartment in apartments)
                {
                    var requests = _context.Requests.Where(r => r.ApartmentId.Equals(apartment.ApartmentId)).ToList();
                    foreach (var request in requests)
                    {
                        if (request.CategoryID == category)
                            requestsResponse.Add(request);
                    }
                    
                }
            }

            return requestsResponse;
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

        public IEnumerable<Request_> GetAllRequestStaff(Guid staffID)
        {
            return _context.Requests.Where(r => r.MaintenanceStaff.ID.Equals(staffID));
        }
    }
}