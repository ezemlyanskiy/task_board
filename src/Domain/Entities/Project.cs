namespace Domain.Entities;

public class Project(string title, string description)
{
    public int Id { get; set; }
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
}
