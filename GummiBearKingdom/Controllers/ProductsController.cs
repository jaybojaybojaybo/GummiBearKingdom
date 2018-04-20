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
    public class ProductsController : Controller
    {
        private readonly GummiDbContext _context;

        public ProductsController(GummiDbContext context)
        {
            _context = context;
        }



        // GET: Product
        public async Task<IActionResult> Index()
        {
            var gummiDbContext = _context.Products.Include(p => p.Category);
            return View(await gummiDbContext.ToListAsync());
        }

        //GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        //POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductId, Name, Description, Price, CatagoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }
    }
}
