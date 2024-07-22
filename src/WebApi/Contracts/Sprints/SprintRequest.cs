using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Sprints;

public class SprintRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string? Comment { get; set; }
    public Guid ProjectId { get; set; }
    public List<Guid>? UserIds { get; set; }
    public IFormFileCollection? Files { get; set; }
}
