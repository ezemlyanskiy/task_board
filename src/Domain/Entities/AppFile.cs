namespace Domain.Entities;

public class AppFile
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
}
