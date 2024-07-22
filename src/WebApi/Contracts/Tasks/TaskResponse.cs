using WebApi.Contracts.Common;

namespace WebApi.Contracts.Tasks;

public record TaskResponse(
    Guid Id,
    string Title,
    string Description,
    string Status,
    string Comment,
    Guid SprintId,
    Guid UserId,
    List<FileResponse> Files);
