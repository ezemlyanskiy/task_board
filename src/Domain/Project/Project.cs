using Domain.Project.ValueObjects;

namespace Domain.Project;

public sealed class Project : AggregateRoot<ProjectId>
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    private Project(ProjectId projectId, string title, string description)
        : base(projectId)
    {
        Title = title;
        Description = description;
    }

    public static Project Create(string title, string description)
    {
        return new(ProjectId.CreateUnique(), title, description);
    }

    public static Project Update(Project project, string title, string description)
    {
        project.Title = title;
        project.Description = description;

        return project;
    }

#pragma warning disable CS8618
    private Project()
    {
    }
#pragma warning restore CS8618
}
