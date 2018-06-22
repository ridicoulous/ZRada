using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Text;

namespace EtherScan
{
    public class TokenInfo
    {
        [CsvColumn(Name="Name",FieldIndex = 1)]
        public string Name { get; set; }
        [CsvColumn(Name = "Contact", FieldIndex = 2)]
        public string Contact { get; set; }
    }
    public class TokenHolderInfo
    {
        [CsvColumn(Name = "Name", FieldIndex = 1)]
        public string Name { get; set; }
        [CsvColumn(Name = "Holder", FieldIndex = 2)]
        public string Holder { get; set; }
        [CsvColumn(Name = "Desc", FieldIndex = 3)]
        public string Desc { get; set; }
        [CsvColumn(Name = "Volume", FieldIndex = 4)]
        public string Volume { get; set; }
        [CsvColumn(Name = "Percents", FieldIndex = 5)]
        public string Percents { get; set; }
    }


}
