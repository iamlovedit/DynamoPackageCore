using Newtonsoft.Json;

namespace DynamoPackageService.Infrastructure;

public class MessageModel<T>
{
    [JsonProperty("code")] public int StatusCode { get; set; } = 200;
    [JsonProperty("success")] public bool IsSuccess { get; }
    [JsonProperty("message")] public string Msg { get; }
    [JsonProperty("response")] public T Response { get; }

    public MessageModel(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Msg = message;
    }

    public MessageModel(bool isSuccess, string message, T response) : this(isSuccess, message)
    {
        Response = response;
    }

    public static MessageModel<T?> Success(string message)
    {
        return new MessageModel<T?>(true, message, default);
    }

    public static MessageModel<T> Success(string message, T response)
    {
        return new MessageModel<T>(true, message, response);
    }

    public static MessageModel<T> Fail(string message, T response)
    {
        return new MessageModel<T>(false, message, response);
    }

    public static MessageModel<T> Message(bool isSuccess, string message, T response)
    {
        return new MessageModel<T>(isSuccess, message, response);
    }
}