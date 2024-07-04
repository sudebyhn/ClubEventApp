using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Dto
{
    public class SaveImageRequestDto
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public byte[] Data { get; set; }
    }
}
