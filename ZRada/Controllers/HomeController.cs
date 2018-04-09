
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZRada.Data;
using ZRada.Helpers;
using ZRada.Models;

namespace ZRada.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return Redirect("Votings");
            //var listOfViewComponents = Assembly
            //                .GetEntryAssembly()
            //                .GetTypes()
            //                .Where(x => x.GetTypeInfo().BaseType == typeof(ViewComponent));
            //Dictionary<string, string> components = new Dictionary<string, string>();
            //foreach (var comp in listOfViewComponents)
            //{
            //    var attr = comp.GetCustomAttribute<ComponentDescriptionAttribute>();
            //    components.Add(attr.Name, comp.ToString());

            //}
            //return View(components);
        }

        public async Task<IActionResult> Invoke(string component)
        {
            var splitted = component.Split('.');
           return View(new ComponentInvoker(splitted[splitted.Length-1].Replace("ViewComponent","")));
        }
       

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
