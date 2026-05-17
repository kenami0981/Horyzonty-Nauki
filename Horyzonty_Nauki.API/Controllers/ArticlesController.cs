using Horyzonty_Nauki.Application;
using Horyzonty_Nauki.Application.Article;
using Horyzonty_Nauki.Application.Articles;
using Horyzonty_Nauki.Domain;
using Horyzonty_Nauki.Infrastructure.Data;
using MediatR;
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
        [HttpGet("{id}")] //api/books/{id}
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
    }
}