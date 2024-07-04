﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class UpdateEventRequestDto
    {
        public Guid EventId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int EventSituationId { get; set; }
        public string FacultyName { get; set; }
        public DateTime EventDate { get; set; }
        public Guid SksAdminId { get; set; }

    }
}
