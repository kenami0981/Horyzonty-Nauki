using Horyzonty_Nauki.Application.Attachments;
using Horyzonty_Nauki.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Horyzonty_Nauki.Application.Articles;

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
        /*
        [HttpGet("{id}")]
            public async Task<IActionResult> DownloadArticle(
                Guid id)
            {
                var attachment = await _context.Attachments
                    .FirstOrDefaultAsync(
                        x => x.Id_Article == id);

                if (attachment == null)
                {
                    return NotFound();
                }

                var file =
                    await _fileStorageService.GetAsync(
                        attachment.File_path);

                if (file == null)
                {
                    return NotFound();
                }

                return File(
                    file.Content,
                    file.ContentType,
                    file.FileName);
            }
         */
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")] 
        public async Task<IActionResult> EditAttachment(Guid id, AttachmentDto attachment)
        {
            var command = new AttachmentEdit.Command
            {
                Id = id,
                AttachmentsCreateDto = attachment
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
        public async Task<ActionResult> CreateAttachment(AttachmentDto attachment)
        {
            var result = await _mediator.Send(new AttachmentCreate.Command { AttachmentsCreateDto = attachment });
            if (result == null)
            {
                return BadRequest();
            }
            if (result.IsSuccess && result.Value != null)
            {
                return CreatedAtAction(nameof(GetAttachment), new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.ErrorMessage);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteAttachment(Guid id)
        {
            var result = await _mediator.Send(new AttachmentDelete.Command { Id = id });
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
        [HttpPost("{id}/open")]
        public async Task<IActionResult> IncreaseOpenCount(Guid id)
        {
            var result = await _mediator.Send(new ArticleIncreaseOpenCount.Command
            {
                Id = id
            });

            if (result.IsSuccess)
                return Ok();

            return NotFound(result.ErrorMessage);
        }

    }
}
