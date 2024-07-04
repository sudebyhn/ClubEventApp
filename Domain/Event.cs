using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class Event
    {
        public Guid EventId { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public DateTime EventCreateDate { get; private set; }
        public List<string> FacultyName { get; private set; }
        public byte[] Image { get; private set; }

        public ProcessSituationEnum ProcessSituation { get; private set; }
        public int EventSituationId
        {
            get { return (int)this.ProcessSituation; }
            set { ProcessSituation = (ProcessSituationEnum)value; }
        }

        private Event()
        {
            
        }
        public Event(string title)
        {
            this.EventId = Guid.NewGuid();
            this.Title = title;
            this.EventCreateDate = DateTime.Now;
        }
    }
}
