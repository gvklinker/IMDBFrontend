using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBFrontend
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nconst { get; set; }
        public List<string> Professions { get; set; }
        public int BirthYear { get; set; }
        public int DeathYear { get; set; }
        public Person() {
            Professions = new List<string>();
        }
    }
}
