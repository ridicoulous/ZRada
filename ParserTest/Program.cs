
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using ZRada.Data;

namespace ParserTest
{
    class Program
    {

        static void Main(string[] args)
        {
            for (int i = 1; i < 17700; i++)
            {

                ParsePage(i);
              //  new Thread(() => { ParsePage(i); }).Start();
            }
        }
        private static List<List<T>> CustomSplit<T>(List<T> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 2)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        private static void ParsePage(int id)
        {
            try
            {
                var parser = new HtmlParser();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1252 = Encoding.GetEncoding("WINDOWS-1251");
                using (var wc = new WebClient() { Encoding = enc1252 })
                {
                    var e = Encoding.GetEncodings().ToList();
                    var bytes = wc.DownloadData($"http://w1.c1.rada.gov.ua/pls/radan_gs09/ns_golos?g_id={id}");
                    string responseString = enc1252.GetString(bytes, 0, bytes.Length);
                    var doc = parser.Parse(responseString);

                    var header = doc.QuerySelector("div.head_gol");
                    var res = header.TextContent;
                    using (var db = new ApplicationDbContext())
                    {
                        var voting = new Voting() { Name = res, Number=id };
                        db.Votings.Add(voting);
                        db.SaveChanges();

                        var emphasize = doc.QuerySelectorAll("div.dep, div.golos").ToList();
                        var t = CustomSplit(emphasize);
                        List<Deps> list = t.Take(t.Count / 2).Select(c => new Deps(c[0].Text(), c[1].Text())).ToList();
                        foreach(var vote in list)
                        {
                            db.Votes.Add(new Vote(voting.Id,GetDeputatId(vote.Name,db),vote.Vote));
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public class Deps
        {
            public Deps(string name, string vote)
            {
                Name = name;
                Vote = vote;
            }
            public string Name { get; set; }
            public string Vote { get; set; }
        }
        
        private static string GetDeputatId(string deputat, ApplicationDbContext db)
        {
            string id = db.Deputats.FirstOrDefault(c => c.Name == deputat)?.Id;
            if (String.IsNullOrEmpty(id))
            {
                var d = new Deputat() { Name = deputat };
                db.Deputats.Add(d);
                db.SaveChanges();
                id = d.Id;
                return id;
            }

            return id;

        }


    }
}
