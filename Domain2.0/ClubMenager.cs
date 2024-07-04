using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class ClubMenager
    {
        public Guid ClubManagerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

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
