using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBFrontend
{
    public class Title
    {
        public string PrimaryTitle { get; set; }
        public string? OriginalTitle { get; set; }
        public string Tconst { get; set; }
        public string? Type { get; set; }
        public string Genres { get; set; }
        public string StartYear { get; set; }
        public string? EndYear { get; set; }
        public string? RunTimeMinutes { get; set; }
        public Title() {
            
        }

        public override string ToString()
        {
            return $"{Tconst} {PrimaryTitle} {OriginalTitle} \n " +
                $"{Type} {Genres}" +
                $"{StartYear} {EndYear}";
        }
    }
}
