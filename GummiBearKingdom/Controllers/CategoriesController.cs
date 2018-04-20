
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GummiBearKingdom.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummiBearKingdom.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly GummiDbContext _context;

        public CategoriesController(GummiDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        //GET: Categories/Details/id
        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories.Include(c => c.Products)
                .SingleOrDefaultAsync(p => p.CategoryId == id);
            return View(category);
        }

        //GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Categories/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryId, Name")] Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
