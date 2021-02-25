using System;

namespace Choreography.Sample.Events
{
    public interface IEvent
    {
        string DataContentType { get; }
        Guid Id { get; set; }
        string Source { get; set; }
        string SpecVersion { get; }
        DateTimeOffset Time { get; set; }
        string Type { get; set; }
    }
}