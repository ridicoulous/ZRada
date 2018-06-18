using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZRada.Data;

namespace ZRada.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChartsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Bar()
        {
            return View();
        }
    }
}