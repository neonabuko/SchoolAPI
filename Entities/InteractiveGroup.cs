namespace WizardAPI.Entities;

public class InteractiveGroup
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public required Teacher Teacher { get; set; }
    public required ICollection<Student> Students { get; set; }
}