using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Horyzonty_Nauki.API.Controllers
{
    public class AttachmentsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public AttachmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Attachment>>> GetAll()
        {

            var attachments = await _mediator.Send(new AttachmentList.Query());

            if (attachments == null || !attachments.IsSuccess)
                return BadRequest();
            return Ok(attachments);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttachmentDto>> GetAttachment(Guid id)
        {
            var result = await _mediator.Send(new AttachmentDetails.Query { Id = id });

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
