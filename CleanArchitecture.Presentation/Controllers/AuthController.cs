using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class AuthController :ApiController
    {
        public AuthController(IMediator mediator):base(mediator)
        {
            
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Register (RegisterCommand reqest,CancellationToken cancellationToken)
        {
            MessageResponse response=await _mediator.Send(reqest, cancellationToken);

            return Ok(response);
        }
    }
}
