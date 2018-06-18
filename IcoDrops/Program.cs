using AngleSharp.Parser.Html;
using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IcoDrops
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parser = new HtmlParser();
                Dictionary<string, string> icos = new Dictionary<string, string>();
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                //      var enc1252 = Encoding.GetEncoding("WINDOWS-1251");
                using (var wc = new WebClient())
                {
                    var e = Encoding.GetEncodings().ToList();
                    var responseString = wc.DownloadString($"https://icodrops.com/category/ended-ico/");
                    // string responseString = enc1252.GetString(bytes, 0, bytes.Length);
                    var doc = parser.Parse(responseString);
                    List<object> objs = new List<object>();
                    var ids = doc.QuerySelectorAll("#ended_ico").SelectMany(f => f.Children);
                    Console.WriteLine("Name;Interest;Type;Cap;Set;Date;Ticker;");
                    foreach (var i in ids)
                    {
                        var type = String.IsNullOrEmpty(i.ClassName) ? i.Id : i.ClassName;
                        var desc = i.TextContent.TrimStart('\t', '\n', '\r');
                        if (type == "ico-row")
                        {
                            desc = desc.Remove(desc.IndexOf("\n"));
                        }
                        if (desc.Contains("\n"))
                        {
                            desc = desc.Remove(desc.IndexOf("\n"));
                        }
                        desc = desc.TrimEnd('\t', '\n', '\r');
                        desc = desc.Trim('\t', '\n', '\r');
                        Console.Write($"{desc};");
                        if(desc.Contains("Ticker"))
                            Console.Write(Environment.NewLine);
                    }
                    Console.WriteLine();

                    Console.WriteLine();

                    var t = JsonConvert.SerializeObject(objs);
                    Console.ReadLine();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }

    public class IcoDropTable
    {
        public IcoDropTable(string name, string interest, string type, string cap, string desc, string tiker)
        {
            Name = name;
            Interest = interest;
            Type = type;
            Cap = cap;
            Desc = desc;
            Ticker = tiker;
        }
        public string Name { get; set; }
        public string Interest { get; set; }

        public string Type { get; set; }
        public string Cap { get; set; }
        public string Desc { get; set; }
        public string Ticker { get; set; }


    }
}
