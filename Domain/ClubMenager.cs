using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class ClubMenager
    {
        public Guid ClubManagerId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        private ClubMenager()
        {
            
        }

        public ClubMenager(string name, string surname)
        {
            this.ClubManagerId = Guid.NewGuid();
            this.Name = name;
            this.Surname = surname;
            this.CreateDate = DateTime.Now;
        }
    }
}
