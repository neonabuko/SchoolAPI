using System.ComponentModel.DataAnnotations;

namespace WizardAPI.Entities;

public class InteractiveClass
{
    public int Id { get; set; }
    [StringLength(20)]
    public required string Lesson { get; set; }
    public required DateTime DateTime { get; set; }
    
    public int? Oral { get; set; }
    public bool? HwDelivered { get; set; }
    public int? HwGrade { get; set; }
    public bool? StudentPresent { get; set; }
    
    public int? StudentId { get; set; }
    public Student? Student { get; set; }
}