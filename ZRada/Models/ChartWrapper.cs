using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZRada.Models
{
    public class ChartWrapper
    {
        public ChartWrapper(string title, string sub, GoogleVisualizationDataTable table)
        {
            Title = title;
            Subtitle = sub;
            DataTable = table;
        }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public GoogleVisualizationDataTable DataTable { get; set; }
    }
}
