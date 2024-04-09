using System.ComponentModel.DataAnnotations;
using SchoolAPI.Entities.Enums;

namespace SchoolAPI.Entities;

public class Lesson
{
    public int Id { get; set; }
    [StringLength(20)]
    public required string Name { get; set; }
    public required DateTime DateTime { get; set; }
    
    public Grades? Oral { get; set; }
    public bool? HwDelivered { get; set; }
    public Grades? HwGrade { get; set; }
    public bool? StudentPresent { get; set; }
    
    public int? StudentId { get; set; }
    public Student? Student { get; set; }
}