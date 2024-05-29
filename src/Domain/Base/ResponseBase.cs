namespace Domain.Base;

public class ResponseBase<T>
{
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }

    public static ResponseBase<T> Success(T result) => new ResponseBase<T> { Result = result };

    public static ResponseBase<T> Error(string errorMessage) => new ResponseBase<T> { ErrorMessage = errorMessage };
}