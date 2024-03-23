using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WizardAPI.Entities;

namespace WizardAPI.Data;

public class WizardContext(DbContextOptions<WizardContext> options) : DbContext(options)
{
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<InteractiveGroup> InteractiveGroups => Set<InteractiveGroup>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<InteractiveClass> WizardClasses => Set<InteractiveClass>();
    public DbSet<Book> Books => Set<Book>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}