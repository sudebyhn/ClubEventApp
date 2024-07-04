using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class CreateClubRequestDto
    {
        public string ClubName { get; set; }
        public string FacultyName { get; set; }
        public Guid SksAdminId { get; set; }
    }
}
