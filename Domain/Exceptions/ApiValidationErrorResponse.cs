namespace Domain.Exceptions;

public class ApiValidationErrorResponse : ExceptionApiResponse
{
    public ApiValidationErrorResponse() : base(400)
    {
    }

    public IEnumerable<String> Errors { get; set; }
}