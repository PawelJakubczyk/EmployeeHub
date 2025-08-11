namespace Domain.Entities;

public class Team
{
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation properties
    public List<Employee> Employees { get; set; } = [];

    public Team(int id, string name)
    {
        TeamId = id;
        Name = name;
    }

    private Team()
    {
        // Parameterless constructor for EF Core
    }
}