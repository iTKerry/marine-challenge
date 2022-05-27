using MediatR.Core.Abstractions;

namespace MediatR.Core;

public class OkHandlerResult : IHandlerResult
{
}

public class DataHandlerResult<T> : IHandlerResult<T>
{
    public T Data { get; }

    public DataHandlerResult(T data) => Data = data;
}

public class PagedDataHandlerResult<T> : IHandlerResult<T>
{
    public T Data { get; }
    public int Count { get; }

    public PagedDataHandlerResult(T data, int count)
    {
        Data = data;
        Count = count;
    }
}

public class NotFoundHandlerResult<T> : IHandlerResult<T>
{
}

public class NotFoundHandlerResult : IHandlerResult
{
}

public class ValidationFailedHandlerResult<T> : IHandlerResult<T>
{
    public string Message { get; }

    public ValidationFailedHandlerResult(string message) => Message = message;
}

public class ValidationFailedHandlerResult : IHandlerResult
{
    public string Message { get; }

    public ValidationFailedHandlerResult(string message) => Message = message;
}