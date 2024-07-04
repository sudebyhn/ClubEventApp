using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class DeleteEventRequestDto
    {
        public Guid EventId { get; set; }
        public Guid SksAdminId { get; set; }
    }
}
