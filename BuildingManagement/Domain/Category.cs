using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Category
{
    public string Name{ get; set; }
    public string Description { get; set; }
    [Key]
    public int ID { get; set; }
}