using System.ComponentModel.DataAnnotations;

namespace WizardAPI.Entities;

public class Teacher
{
    public int Id { get; set; }
    [StringLength(50)] 
    public string Name { get; set; }
    public DateOnly Birthday { get; set; }
    
    public ICollection<InteractiveGroup>? InteractiveGroups { get; set; }
}