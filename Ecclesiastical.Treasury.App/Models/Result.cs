namespace Ecclesiastical.Treasury.App.Models;

public class Result
{
    public Result(string? message)
    {
        this.Message = message;
        this.Error = message is not null;
    }

    public Result() { }
    public string? Message { get; set; }
    public bool Error { get; set; }
    
    public static implicit operator bool(Result result) => result.Error;
}

public class DataResult<T> : Result
{    
    public DataResult(string message) : base(message)
    {

    }

    public DataResult(T data)
    {
        Data = data;
    }

    public T? Data { get; set; }
}