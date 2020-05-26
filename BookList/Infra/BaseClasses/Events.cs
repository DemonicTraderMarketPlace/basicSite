using System;

namespace BookList.Infra.BaseClasses
{
    public class Events
    {
        public string Id { get; set; }
        public string StreamId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
