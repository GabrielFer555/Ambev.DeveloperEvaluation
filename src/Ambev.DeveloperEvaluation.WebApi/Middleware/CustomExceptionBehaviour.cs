﻿namespace Ambev.DeveloperEvaluation.Common.Behaviours;
public class CustomExceptionHandler
	(ILogger<CustomExceptionHandler> logger)
	: IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
	{
		logger.LogError(
			"Error Message: {exceptionMessage}, Time of occurrence {time}",
			exception.Message, DateTime.UtcNow);

		(string Detail, string Title, int StatusCode) details = exception switch
		{
			InternalServerErrorException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			),
			ValidationException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status400BadRequest
			),
			BadRequestException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status400BadRequest
			),
			NotFoundException =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status404NotFound
			),
			_ =>
			(
				exception.Message,
				exception.GetType().Name,
				context.Response.StatusCode = StatusCodes.Status500InternalServerError
			)
		};

		var problemDetails = new ProblemDetails
		{
			Title = details.Title,
			Detail = details.Detail,
			Status = details.StatusCode
		};


		if (exception is ValidationException validationException)
		{
			problemDetails.Detail = validationException.Errors.ToArray()[0].ErrorMessage;
		}

		await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
		return true;
	}
}