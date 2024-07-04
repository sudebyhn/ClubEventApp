using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class Club
    {
        public Guid ClubId { get; private set; }
        public string ClubName { get;private set; }
        public ClubMenager ClubMenager { get;private set; }
        public DateTime CreateDate { get; private set; }
        public byte[] Image { get; private set; }
        public List<string> FacultyName { get; private set; }

        private Club()
        {
            
        }

        public Club(string clubName)
        {
            this.ClubId = Guid.NewGuid();
            this.ClubName = clubName;
            this.CreateDate=DateTime.Now;
        }
    }
}
