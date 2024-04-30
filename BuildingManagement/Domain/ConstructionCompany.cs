
using System.ComponentModel.DataAnnotations;
namespace Domain
{
    public class ConstructionCompany
    {
        [Key]
        public Guid CompanyId { get; set; } 
        public string Name { get; set; }
    }
}
