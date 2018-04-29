using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GummiBearKingdom.Models;
using GummiBearKingdom.Controllers;

namespace GummiBearKingdom.Controllers
{
    public class HomeController : Controller
    {
        private readonly GummiDbContext _context;

        public HomeController(GummiDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var gummiDbContext = _context.Products
                .Include(p => p.Category)
                .Include(r => r.Reviews);
            return View(await gummiDbContext.ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "If you need any assistance, please contact the departments below.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
