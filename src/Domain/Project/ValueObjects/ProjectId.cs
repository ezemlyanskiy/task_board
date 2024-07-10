namespace Domain.Project.ValueObjects;

public sealed class ProjectId : ValueObject
{
    public Guid Value { get; private set; }

    private ProjectId(Guid value)
    {
        Value = value;
    }

    public static ProjectId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProjectId Create(Guid value)
    {
        return new ProjectId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
