using CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Login;
using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class AuthController :ApiController
    {
        public AuthController(IMediator mediator):base(mediator)
        {
            
        }

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register (RegisterCommand reqest,CancellationToken cancellationToken)
        {
            MessageResponse response=await _mediator.Send(reqest, cancellationToken);

            return Ok(response);
        }

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand reqest, CancellationToken cancellationToken)
        {
            LoginCommandResponse response = await _mediator.Send(reqest, cancellationToken);

            return Ok(response);
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateNewTokenByRefreshTokenCommand reqest, CancellationToken cancellationToken)
        {
            LoginCommandResponse response = await _mediator.Send(reqest, cancellationToken);

            return Ok(response);
        }
    }
}
