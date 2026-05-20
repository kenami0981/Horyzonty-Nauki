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
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditConfig(Guid id,ConfigCreateDto config)
        {
            var command = new ConfigEdit.Command
            {
                Id = id,
                ConfigsCreateDto = config
            };

            var result = await _mediator.Send(command);

            if (result == null) return NotFound();

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateConfig(ConfigCreateDto config)
        {
            var result = await _mediator.Send(new ConfigCreate.Command { ConfigsCreateDto = config });
            if (result == null)
            {
                return BadRequest();
            }
            if (result.IsSuccess && result.Value != null)
            {
                return CreatedAtAction(nameof(GetConfig), new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(Guid id)
        {
            var result = await _mediator.Send(new ConfigDelete.Command { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
