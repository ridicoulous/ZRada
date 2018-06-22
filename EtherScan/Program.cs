using AngleSharp.Parser.Html;
using DSharp.CsvHelper;
using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EtherScan
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TokenHolderInfo> holders = new List<TokenHolderInfo>();
            var tokens = ReadCsv();
            foreach (var t in tokens)
            {
                holders.AddRange(GetHolders(t.Contact, t.Name));

            }
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            cc.Write(holders,"C:\\csv\\holder.csv", inputFileDescription);
        }

        public static IEnumerable<TokenInfo> ReadCsv()
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',
                FirstLineHasColumnNames = true
            };
            CsvContext cc = new CsvContext();
            return cc.Read<TokenInfo>("C:\\csv\\tkns.csv", inputFileDescription).ToList();
        }

        public static List<TokenHolderInfo> GetHolders(string tokenAddress, string name)
        {
            var list = new List<TokenHolderInfo>();
            var parser = new HtmlParser();
            string url = $"https://etherscan.io/token/tokenholderchart/{tokenAddress}";
            using (var wc = new WebClient())
            {
                var responseString = wc.DownloadString(url);
                // string responseString = enc1252.GetString(bytes, 0, bytes.Length);
                var doc = parser.Parse(responseString);

                AngleSharp.Dom.Html.IHtmlTableElement table = (AngleSharp.Dom.Html.IHtmlTableElement)doc.QuerySelectorAll("table").Where(c => c.InnerHtml.Contains("Address")).FirstOrDefault();
                var rows = table.Rows;
                foreach (var row in rows)
                {
                    var desc = "";
                    var h = row.Cells[1].TextContent;
                    if (row.Cells[1].TextContent.Split(' ').Count() > 1)
                    {
                        desc = row.Cells[1].TextContent.Split(' ')[1];
                        h = row.Cells[1].TextContent.Split(' ')[0];
                    }
                    var holder = new TokenHolderInfo() { Desc = desc, Holder = h, Name = name, Percents = row.Cells[3].TextContent, Volume = row.Cells[2].TextContent };
                    list.Add(holder);
                    Console.WriteLine(row.Cells[0].TextContent + " " + row.Cells[1].TextContent + " " + row.Cells[2].TextContent + " " + row.Cells[3].TextContent);

                }
                return list;



            }


        }


        public static void Write(string name, string createText)
        {
            File.WriteAllText("C:\\csv\\" + name, createText);
        }
        private static /*Dictionary<string, string>*/string GetTokenInfo(string tokenContract)
        {
            var parser = new HtmlParser();

            string url = $"https://etherscan.io/token/{tokenContract}#tokenInfo";

            using (var wc = new WebClient())
            {
                var responseString = wc.DownloadString(url);
                // string responseString = enc1252.GetString(bytes, 0, bytes.Length);
                var doc = parser.Parse(responseString);

                var text = doc.QuerySelectorAll("table").Where(c => c.InnerHtml.Contains("Start Date")).FirstOrDefault()?.TextContent ?? "";
                if (!String.IsNullOrEmpty(text))
                {
                    List<char> replaced = new List<char>();
                    string res1 = text.Replace("\n\n", "");
                    return res1.Replace("\n:\n", ":").Trim('\n');
                }
                return "";


            }

        }


        private static Dictionary<string, string> GetTokens(int pageId)
        {
            var parser = new HtmlParser();
            Dictionary<string, string> tokensList = new Dictionary<string, string>();
            using (var wc = new WebClient())
            {

                var responseString = wc.DownloadString($"https://etherscan.io/tokens?p={pageId}");
                // string responseString = enc1252.GetString(bytes, 0, bytes.Length);
                var doc = parser.Parse(responseString);

                var ids = doc.QuerySelectorAll("a").Where(c => c.Attributes.Any(a => a.Value.Contains("token/") && !c.InnerHtml.Contains("img")));

                foreach (var id in ids)
                    tokensList.TryAdd(id.InnerHtml, id.GetAttribute("href").Replace("/token/", ""));

                return tokensList;
            }
        }
    }
}

/*
    Dictionary<string, string> tokensList = new Dictionary<string, string>();
            for (int i = 1; i <= 11; i++)
            {
            foreach (var r in GetTokens(i))
            {
                tokensList.TryAdd(r.Key, r.Value);
            }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Name;Contact;ICO Start Date;ICO End Date;ICO Price;Total Raised;Country");
            sb.Append(Environment.NewLine);
            foreach (var tr in tokensList)
            {
                string res = tr.Key+";"+tr.Value;
                var info = GetTokenInfo(tr.Value);
                if(!String.IsNullOrEmpty(info))
                {
                    try
                    {
                        foreach (var entry in info.Split('\n'))
                        {
                            res += ";" + entry.Split(':')[1];
                        }
                        Console.WriteLine(res);
                      
                    }
                    catch (Exception)
                    {
                        res += ";empty;empty;empty;empty";
                        
                    }
                    finally
                    {
                        sb.Append(res);
                        sb.Append(Environment.NewLine);
                    }
                }
            }
            Write("tokens.csv", sb.ToString());


            //Console.WriteLine("Name,Link");
            //foreach (var item in tokensList)
            //{
            //    Console.WriteLine($"{item.Key},{item.Value}");
            //}



            GetTokenInfo("0x86fa049857e0209aa7d9e616f7eb3b3b78ecfdb0");
            Console.ReadLine();  
    */
