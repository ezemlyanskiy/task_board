namespace WebApi.Contracts.Tasks;

public class TaskRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? Comment { get; set; }
    public Guid SprintId { get; set; }
    public Guid? UserId { get; set; }
    public IFormFileCollection? Files { get; set; }
}
