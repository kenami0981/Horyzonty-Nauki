using Horyzonty_Nauki.Application.Configs;
using Horyzonty_Nauki.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Horyzonty_Nauki.API.Controllers
{
    public class ConfigsController:BaseApiController
    {
        private readonly IMediator _mediator;
        public ConfigsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Config>>> GetAll()
        {

            var configs= await _mediator.Send(new ConfigList.Query());

            if (configs == null || !configs.IsSuccess)
                return BadRequest();
            return Ok(configs);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigDto>> GetConfig(Guid id)
        {
            var result = await _mediator.Send(new ConfigDetails.Query { Id = id });

            if (result == null || result.Value == null)
            {
                return NotFound();
            }

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.ErrorMessage);

        }
       
    }
}
