using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class Event
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime EventCreateDate { get; private set; }
        public DateTime EventDate { get; set; }
        public string FacultyName { get; set; }
        public Guid? ImageId { get; set; }
        public string? ImageName { get; set; }
        public byte[]? Data { get; set; }
        public ProcessSituationEnum ProcessSituation { get; private set; }
        public int EventSituationId
        {
            get { return (int)this.ProcessSituation; }
            set { ProcessSituation = (ProcessSituationEnum)value; }
        }

        public Event()
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
