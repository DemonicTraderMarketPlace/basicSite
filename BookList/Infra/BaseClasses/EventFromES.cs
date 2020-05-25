using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Infra.BaseClasses
{
    public class EventFromES
    {
        public string Id { get; set; }
        public string TimeStamp { get; set; }
        public string StreamId { get; set; }
        public string EventType { get; set; }
        public dynamic Data { get; set; }
    }
}
