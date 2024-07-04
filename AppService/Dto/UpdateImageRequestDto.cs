using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class UpdateImageRequestDto
    {
        public Guid ImageId { get; set; }
        public string ImageName { get; set; }
        public byte[] Data { get; set; }
    }
}
