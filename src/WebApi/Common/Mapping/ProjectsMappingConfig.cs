using Application.Projects.Commands.Create;
using Application.Projects.Commands.Update;
using Domain.Entities;
using Mapster;
using WebApi.Contracts.Projects;

namespace WebApi.Common.Mapping;

public class ProjectsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUpdateProjectRequest, CreateProjectCommand>();

        config.NewConfig<(CreateUpdateProjectRequest Request, int ProjectId), UpdateProjectCommand>()
            .Map(dest => dest.Id, src => src.ProjectId)
            .Map(dest => dest, src => src.Request);
        
        config.NewConfig<Project, ProjectReponse>();
    }
}