using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Entities;

public class Teacher
{
    public int Id { get; set; }
    [StringLength(50)] 
    public required string Name { get; set; }
    public DateOnly Birthday { get; set; }
    
    public ICollection<Group>? Groups { get; set; }
}