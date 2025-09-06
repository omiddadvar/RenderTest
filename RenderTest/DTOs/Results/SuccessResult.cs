namespace RenderTest.DTOs.Results;

public class SuccessResult
{
    public const bool IsSuccess = true;
    public object Data {  get; set; }
    public string Message { get; set; } = "Successful";
}
