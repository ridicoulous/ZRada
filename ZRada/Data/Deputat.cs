using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZRada.Data
{
    public class Deputat : Base
    {
        public string Name { get; set; }
        public DateTime FirstVote { get; set; }
        public DateTime LastVote { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
