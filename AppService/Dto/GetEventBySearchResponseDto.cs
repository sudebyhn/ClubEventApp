using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class GetEventBySearchResponseDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime EventDate { get; set; }
        public string FacultyName { get; set; }
        public int EventSituationId { get ; set; }
    }
}
