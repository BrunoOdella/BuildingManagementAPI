using Domain;

namespace IDataAccess;

public interface IRequestRepository
{
    Request_ CreateRequest(Request_ request);
}