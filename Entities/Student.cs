using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Entities;

public class Student
{
    public int Id { get; set; }
    [StringLength(50)]
    public required string Name { get; set; }
    public required DateOnly Birthday { get; set; }
    public int? RegistrationId { get; set; }

    public int? GroupId { get; set; }
    public Group? Group { get; set; }
    public ICollection<Lesson>? Lessones { get; set; }
}