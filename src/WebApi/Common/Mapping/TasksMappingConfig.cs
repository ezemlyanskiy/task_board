using Application.Tasks.Common;
using Mapster;
using WebApi.Contracts.Tasks;

namespace WebApi.Common.Mapping;

public class TasksMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TaskResult, TaskResponse>();
    }
}
