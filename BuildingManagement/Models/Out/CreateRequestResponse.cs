using Domain;

namespace Models.Out;

public class CreateRequestResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public int Category { get; set; }
    public DateTime CreationTime { get; set; }
    public Status Status { get; set; }
    public Guid ApartmentID { get; set; }

    public CreateRequestResponse(Request_ request)
    {
        this.Id = request.Id;
        this.Description = request.Description;
        this.Category = (int)request.CategoryID;
        this.CreationTime = request.CreationTime;
        this.Status = request.Status;
        this.ApartmentID = request.Apartment.ApartmentId;
    }

    public override bool Equals(object obj)
    {
        return obj is CreateRequestResponse response &&
               Id == response.Id;
    }
}