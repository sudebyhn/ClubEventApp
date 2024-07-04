using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class Club
    {
        public Guid ClubId { get; set; }
        public string ClubName { get; set; }
        public DateTime CreateDate { get; private set; }
        public Guid? ImageId { get; set; }
        public string? ImageName { get; set; }
        public byte[]? Data { get; set; }
        public string FacultyName { get; set; }

        public Club()
        {

        }

        public Club(string clubName, string facultyName)
        {
            this.ClubId = Guid.NewGuid();
            this.ClubName = clubName;
            this.CreateDate = DateTime.Now;
            this.FacultyName = facultyName;
        }
    }
}
