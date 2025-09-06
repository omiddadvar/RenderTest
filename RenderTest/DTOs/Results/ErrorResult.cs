namespace RenderTest.DTOs.Results;

public class ErrorResult
{
    public const bool IsSuccess = false;
    public string Message { get; set; }
    public string StackTrace { get; set; }
}
