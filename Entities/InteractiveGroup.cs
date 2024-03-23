using System.ComponentModel.DataAnnotations;

namespace WizardAPI.Entities;

public class InteractiveGroup
{
    public int Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    public string DaysOfTheWeek { get; set; }
    public TimeOnly Time { get; set; }
    
    public int? TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public ICollection<Student>? Students { get; set; }
}