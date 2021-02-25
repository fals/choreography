using System;

namespace Choreography.Sample.Events
{
    public class SomethingHappened : Event<SomeData>
    {
        public SomethingHappened() : base("com.example.somethinghappened")
        {

        }
    }

    public class SomeData
    {
        public int Number { get; set; }
        public string Message { get; set; }
        public DateTime Received { get; set; }
    }
}
