using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CardStorageService.Abstractions.ResponseTools
{
    [DataContract]
    public class Result<TValue>
        where TValue : class
    {
        [JsonIgnore]
        public object EventSource { get; }

        [JsonIgnore]
        public string EventSourceMember { get; }

        [DataMember]
        public TValue Value { get; }

        [JsonIgnore]
        public MessageList MessageList { get; }

        [DataMember]
        public IList<string>? Messages => MessageList?.Messages;

        [DataMember]
        public bool IsSuccess { get; }

        [JsonIgnore]
        public Exception Exception { get; }

        private Result(bool isSuccess, TValue value, object eventSource, string eventSourceMember, MessageList message, Exception ex)
        {
            IsSuccess = isSuccess;
            if (isSuccess)
            {
                Value = value;
                MessageList = message;
            }
            else
            {
                EventSource = eventSource;
                EventSourceMember = eventSourceMember;
                MessageList = message;
                Exception = ex == null ? new Exception("Error") : ex;
            }
        }

        public static Result<TValue> Fail(object eventSource, string errorMessage, Exception ex = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            var message = new MessageList();

            message.Add($"{eventSource.GetType().Name}:{memberName} - {errorMessage}");

            return new Result<TValue>(false, null, eventSource, memberName, message, ex);
        }

        public static Result<TValue> Fail(object eventSource, MessageList message, Exception ex = null, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!message.HasMessages())
            {
                throw new ArgumentException(message.ToString(), nameof(message));
            }

            return new Result<TValue>(false, null, eventSource, memberName, message, ex);
        }

        public static Result<TValue> Success(TValue value, MessageList messageList = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new Result<TValue>(true, value, null, null, messageList, null);
        }
    }
}