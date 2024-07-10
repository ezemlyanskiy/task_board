using Domain.Project;

namespace WebApi.Common.Mapping;

public class ProjectsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectRequest, CreateProjectCommand>();

        config.NewConfig<(Guid Id, ProjectRequest Request), UpdateProjectCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Project, ProjectResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest, src => src);
    }
}
