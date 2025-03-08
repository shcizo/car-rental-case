using CarRental.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToMinimalApiResult(this Result result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            ResultStatus.Invalid => Results.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = result.Error,
            }),
            ResultStatus.NotFound => Results.NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Detail = result.Error,
            }),
            _ => Results.InternalServerError(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = result.Error,
            }),
        };
    }
    
    public static IResult ToMinimalApiResult<T>(this Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            ResultStatus.Invalid => Results.BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = result.Error,
            }),
            ResultStatus.NotFound => Results.NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Detail = result.Error,
            }),
            _ => Results.InternalServerError(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = result.Error,
            }),
        };
    }
}