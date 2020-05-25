using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Infra.BaseClasses
{
    public abstract class ReadModel
    {
        public abstract dynamic EventPublish(EventFromES anEvent, List<dynamic> calendar);
    }
}
