namespace WebApi.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController: ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}
