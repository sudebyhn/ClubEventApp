using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class SksAdmin
    {
        public Guid SksAdminId { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        private SksAdmin()
        {

        }

        public SksAdmin(string name, string surname)
        {
            SksAdminId = Guid.NewGuid();
            Name = name;
            Surname = surname;
        }
    }
}
