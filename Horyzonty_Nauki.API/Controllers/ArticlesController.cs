using Horyzonty_Nauki.Application.Article;
using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horyzonty_Nauki.API.Controllers
{
    public class ArticlesController : BaseApiController
    {

        private readonly IMediator _mediator;
        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Article>>> GetAll()
        {

            var articles =  await _mediator.Send(new ArticleList.Query());

            if (articles == null || !articles.IsSuccess)
                return BadRequest();
            return Ok(articles);

        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<ArticleDto>> GetArticle(Guid id)
        {
            var result = await _mediator.Send(new ArticleDetails.Query { Id = id });

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
        public async Task<IActionResult> EditArticle(Guid id, ArticleCreateDto article)
        {
            var command = new ArticleEdit.Command
            {
                Id = id,
                ArticlesCreateDto = article
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
        public async Task<ActionResult> CreateArticle(
            [FromForm] ArticleCreateDto article)
        {
            var result = await _mediator.Send(new ArticleCreate.Command { ArticleCreateDto = article });
            if (result == null)
            {
                return BadRequest();
            }
            if (result.IsSuccess && result.Value != null)
            {
                return CreatedAtAction(nameof(GetArticle), new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.ErrorMessage);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var result = await _mediator.Send(new ArticleDelete.Command { Id = id });
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