using Application.Sprints.Commands.CreateSprint;
using Application.Sprints.Common;
using Mapster;
using WebApi.Contracts.Common;
using WebApi.Contracts.Sprints;

namespace WebApi.Common.Mapping;

public class SprintsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SprintResponse, SprintResult>()
            .Map(dest => dest.UsersIds, src => src.UserIds)
            .Map(dest => dest.Tasks, src => src.Tasks)
            .Map(dest => dest.Files, src => src.Files);

        config.NewConfig<SprintTaskResponse, SprintTaskResult>();
        config.NewConfig<FileResponse, SprintFileResult>();
    }
}
