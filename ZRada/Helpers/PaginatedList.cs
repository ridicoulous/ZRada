using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZRada.Helpers
{
    public class ComponentDescriptionAttribute:Attribute
    {
        public string Name { get; set; }
        public string Params { get; set; }

    }
    public class ComponentInvoker
    {
        public ComponentInvoker(string name)
        { Name = name; }
        public string Name { get; set; }
    }
    public class DaysDropdown
    {
        public DaysDropdown(string name, int val)
        {
            Value = val;
            Name = name;
        }
      
        public int Value { get; set; }
        public string Name { get; set; }
    }

    
}
