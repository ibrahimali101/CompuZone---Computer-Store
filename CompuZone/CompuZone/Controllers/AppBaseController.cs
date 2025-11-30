using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[Route("api/[controller]")] // Standard REST Route
[ApiController]
public class AppBaseController : ControllerBase
{
    private IMediator _mediatorInstance;

    // This allows us to access _mediator without injecting it in every constructor
    protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
}