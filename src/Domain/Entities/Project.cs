namespace Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<Guid>? UserIds { get; set; }
    public List<Sprint>? Sprints { get; set; }
}
