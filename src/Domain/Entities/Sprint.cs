namespace Domain.Entities;

public class Sprint
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Comment { get; set; }
    public Project Project { get; set; } = null!;
    public Guid ProjectId { get; set; }
    public List<Guid>? UserIds { get; set; }
    public List<AppTask>? Tasks { get; set; }
    public List<AppFile>? Files { get; set; }
}
