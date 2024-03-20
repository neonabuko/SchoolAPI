using System.ComponentModel.DataAnnotations;

namespace WizardAPI.Entities;

public class Teacher
{
    public int Id { get; set; }
    [StringLength(50)] 
    public required string Name { get; set; }
    public required DateOnly Birthday { get; set; }
    
    public ICollection<InteractiveGroup>? Groups { get; set; }
}