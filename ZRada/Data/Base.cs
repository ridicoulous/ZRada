using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZRada.Data
{
    public class Base
    {
        public Base()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
