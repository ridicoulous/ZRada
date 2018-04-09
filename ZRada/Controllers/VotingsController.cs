using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using ZRada.Data;
using ZRada.Helpers;

namespace ZRada.Controllers
{
    public class VotingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VotingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, string from = null, string to = null, int? hourFrom = null, int? hourTo = null, int? day = null)
        {
            var qry = _context.Votings.AsNoTracking();
            if (!String.IsNullOrEmpty(from))
            {
                var dateFrom = DateTime.ParseExact(from, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                qry = qry.Where(c => c.Date > dateFrom);
                ViewBag.From = from;

            }
            if (!String.IsNullOrEmpty(to))
            {
                var dateTo = DateTime.ParseExact(to, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                qry = qry.Where(c => c.Date < dateTo);
                ViewBag.To = to;
            }
            if(hourFrom.HasValue&&hourTo.HasValue)
            {
                qry = qry.Where(c => c.Date.Hour > hourFrom||c.Date.Hour<hourTo);
                ViewBag.HourFrom = hourFrom;
                ViewBag.HourTo = hourTo;
            }
            if (hourTo.HasValue&&!hourFrom.HasValue)
            {
                qry = qry.Where(c => c.Date.Hour < hourTo);
                ViewBag.HourTo = hourTo;
            }
            if (!hourTo.HasValue && hourFrom.HasValue)
            {
                qry = qry.Where(c => c.Date.Hour < hourTo);
                ViewBag.HourTo = hourTo;
            }
            if (day.HasValue&&day>=0)
            {
                qry = qry.Where(c => (int)c.Date.DayOfWeek == day.Value);              
            }
           

            ViewBag.Day = new SelectList(GetDays(),"Value","Name");

            ViewBag.Count = qry.Count();
            var model = await PagingList.CreateAsync(qry.OrderBy(p => p.Number), 20, page);
            model.RouteValue = new RouteValueDictionary {
                { "from", from},
                { "to", to },
                { "hourFrom", hourFrom },
                { "hourTo", hourTo },
                { "day",day}


            };
            return View(model);
        }
        public static List<DaysDropdown> GetDays()
        {
            return new List<DaysDropdown>
        {
            new DaysDropdown("По всем дням недели", -1),
            new DaysDropdown("Понедельник", 1),
            new DaysDropdown("Вторник", 2),
            new DaysDropdown("Среда", 3),
            new DaysDropdown("Четверг", 4),
            new DaysDropdown("Пятница", 5),
            new DaysDropdown("Суббота", 6),
            new DaysDropdown("Воскресенье", 0)
        };
        }

        // GET: Votings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _context.Votings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

    }
}
