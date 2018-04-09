using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZRada.Data
{
    public class Voting:Base
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public string ResultsDesc { get; set; }
        public bool Result { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
