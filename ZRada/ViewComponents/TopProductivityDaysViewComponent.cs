using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZRada.Data;
using ZRada.Helpers;

namespace ZRada.ViewComponents
{
    [ComponentDescription(Name ="Количество не/принятых законопроектов по дням",Params ="Принимает количество дней и флаг соответствия")]
    public class ProductivityDaysViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public ProductivityDaysViewComponent(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count, bool isApproved)
        {
            if (count == 0)
                count = 10;
            var items = await GetItemsAsync(count, isApproved);

            return View(items);
        }
        private Task<List<DateTime>> GetItemsAsync(int count=10, bool isApproved=true)
        {
            var votings = db.Votings.Where(c => c.Result == isApproved).GroupBy(c => c.Date.Date)
                    .OrderByDescending(c => c.Count()).Select(f => f.Key);
            return votings.Take(count).ToListAsync();
        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
