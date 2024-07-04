using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class UpdateClubRequestDto
    {
        public Guid Id { get; set; }
        public string ClubName { get; set; }=string.Empty;
        public string FacultyName { get; set; } = string.Empty;

    }
}

