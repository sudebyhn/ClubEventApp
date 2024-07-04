using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class GetClubMenagerBySearchResponseDto
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
    }
}
