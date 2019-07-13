using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieSearch.Core.Exceptions;

namespace MovieSearch.Filters
{
    public class CommonExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NotFoundException notFoundException:
                    context.ExceptionHandled = true;
                    context.Result = new NotFoundObjectResult(notFoundException.Message);
                    break;

                case BadRequestException badRequestException:
                    context.ExceptionHandled = true;
                    context.Result = new BadRequestObjectResult(badRequestException.Message);
                    break;

                case UnauthorizedException _:
                    context.ExceptionHandled = true;
                    context.Result = new UnauthorizedObjectResult(context.Exception.Message);
                    break;

                case ForbiddenException _:
                    context.ExceptionHandled = true;
                    context.Result = new ForbidResult(context.Exception.Message);
                    break;
            }

            base.OnException(context);
        }
    }
}