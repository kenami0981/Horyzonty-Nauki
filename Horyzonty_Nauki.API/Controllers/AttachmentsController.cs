using Horyzonty_Nauki.Application.Article;
using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<Article>>> GetAll()
        {

            var attachments = await _mediator.Send(new AttachmentsList.Query());

            if (attachments == null || !attachments.IsSuccess)
                return BadRequest();
            return Ok(attachments);

        }

        [HttpGet("{id}")] //api/attachment/{id}
        public async Task<ActionResult<AttachmentDto>> GetArticle(Guid id)
        {
            var result = await _mediator.Send(new AttachmentsDetails.Query { Id = id });

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
