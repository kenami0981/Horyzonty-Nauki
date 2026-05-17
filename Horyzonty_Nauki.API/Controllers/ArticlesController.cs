using Horyzonty_Nauki.Application;
using Horyzonty_Nauki.Application.Article;
using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            var articles =  await _mediator.Send(new ArticlesList.Query());

            if (articles == null || !articles.IsSuccess)
                return BadRequest();
            return Ok(articles);

        }
        [HttpGet("{id}")] //api/article/{id}
        public async Task<ActionResult<ArticleDto>> GetArticle(Guid id)
        {
            var result = await _mediator.Send(new ArticlesDetails.Query { Id = id });

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
        [HttpPut("{id}")] //api/article/id z ciałem JSON obiektu Article
        public async Task<IActionResult> EditArticle(Guid id, ArticlesCreateDto article)
        {
            var command = new ArticlesEdit.Command
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

        //[Authorize(Roles = "Admin")]
        [HttpPost] //api/article
        public async Task<ActionResult> CreateArticle(ArticlesCreateDto article)
        {
            var result = await _mediator.Send(new ArticlesCreate.Command { ArticlesCreateDto = article });
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
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")] //api/articles/id
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var result = await _mediator.Send(new ArticlesDelete.Command { Id = id });
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