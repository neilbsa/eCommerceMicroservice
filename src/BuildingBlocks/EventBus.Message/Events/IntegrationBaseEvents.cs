using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Message.Events
{
    public class IntegrationBaseEvents
    {
        public IntegrationBaseEvents()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
        public IntegrationBaseEvents(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
