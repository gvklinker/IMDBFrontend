using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBFrontend
{
    public class Title
    {
        public int Id { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public string Tconst { get; set; }
        public string Type { get; set; }
        public List<string> Genres { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public Title() {
            Genres = new List<string>();
        }
    }
}
