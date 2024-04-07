using System.ComponentModel.DataAnnotations;

namespace WizardAPI.Entities;

public class Student
{
    public int Id { get; set; }
    [StringLength(50)]
    public required string Name { get; set; }
    public required DateOnly Birthday { get; set; }
    public required int RegistrationId { get; set; }

    public int? InteractiveGroupId { get; set; }
    public InteractiveGroup? InteractiveGroup { get; set; }
    public ICollection<InteractiveClass>? InteractiveClasses { get; set; }
}