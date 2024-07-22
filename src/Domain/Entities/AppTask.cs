using Domain.Enums;

namespace Domain.Entities;

public class AppTask
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Status Status { get; set; }
    public string? Comment { get; set; }
    public Guid? UserId { get; set; }
    public Sprint Sprint { get; set; } = null!;
    public Guid SprintId { get; set; }
    public List<AppFile>? Files { get; set; }  
}
