
using Horyzonty_Nauki.Application.Configs;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var configs= await _mediator.Send(new ConfigsList.Query());

            if (configs == null || !configs.IsSuccess)
                return BadRequest();
            return Ok(configs);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigDto>> GetConfig(Guid id)
        {
            var result = await _mediator.Send(new ConfigsDetails.Query { Id = id });

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
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditConfig(Guid id,Horyzonty_Nauki.Application.Configs.ConfigsCreateDto config)
        {
            var command = new ConfigsEdit.Command
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

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateConfig(Horyzonty_Nauki.Application.Configs.ConfigsCreateDto config)
        {
            var result = await _mediator.Send(new ConfigsCreate.Command { ConfigsCreateDto = config });
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

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(Guid id)
        {
            var result = await _mediator.Send(new ConfigsDelete.Command { Id = id });
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
