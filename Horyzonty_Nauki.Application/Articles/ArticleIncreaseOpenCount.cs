using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horyzonty_Nauki.Application;
using MediatR;

namespace Horyzonty_Nauki.Application.Articles
{
    public class ArticleIncreaseOpenCount
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }
    }
}
