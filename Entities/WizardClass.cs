using System.ComponentModel.DataAnnotations;

namespace WizardAPI.Entities;

public class WizardClass
{
    public int Id { get; set; }
    [StringLength(20)]
    public required string Lesson { get; set; }
    public required DateTime DateTime { get; set; }
    public required int TeacherId { get; set; }
    public int Oral { get; set; }
    public required bool HwDelivered { get; set; }
    public int HwGrade { get; set; }
    public required bool StudentPresent { get; set; }

    public required Student Student { get; set; }
}