using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZRada.Data
{
    public class Vote :Base
    {
        public Vote() { }
        public Vote(string votingId, string deputat, string value)
        {
            DeputatId = deputat;
            VotingId = votingId;
            Value = value;
        }
        
        public string Value { get; set; }
        public string DeputatId { get; set; }
        public virtual Deputat Deputat { get; set; }
        public string VotingId { get; set; }
        public virtual Voting Voting { get; set; }
        public DateTime Date { get; set; }
       
    }
}
