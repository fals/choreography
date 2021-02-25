using System;

namespace Choreography.Sample.Events
{
    public abstract class Event<TData> : IEvent
    {
        public string SpecVersion => "1.0";
        public string DataContentType => "application/json";
        public string Source { get; set; }
        public Guid Id { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Type { get; set; }
        public TData Data { get; set; }
        internal Event() { }

        public Event(string type)
        {
            Id = Guid.NewGuid();
            Time = DateTimeOffset.Now;
            Type = type;
        }
    }
}
