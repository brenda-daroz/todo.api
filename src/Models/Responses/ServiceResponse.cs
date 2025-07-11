public class ServiceResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Operation successful.";
    public T Data { get; set; }
    public List<string> Errors { get; set; }
}