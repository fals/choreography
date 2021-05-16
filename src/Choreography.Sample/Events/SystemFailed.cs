namespace Choreography.Sample.Events
{
    public class SystemFailed : Event<ErrorData>
    {
        public SystemFailed() : base("com.example.somethinghappened")
        {

        }
    }

    public class ErrorData
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public MyErrorType Type { get; set; }
    }

    public enum MyErrorType
    {
        Log,
        Error,
        Warning
    }
}
