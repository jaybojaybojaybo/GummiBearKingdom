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
    public class ReviewsController : Controller
    {
        private readonly GummiDbContext _context;

        public ReviewsController(GummiDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int? productId)
        {
            var gummiDbContext = _context.Reviews.Include(r => r.Product);
            List<Review> reviewList = await gummiDbContext.ToListAsync();
            List<Review> reviewsForProduct = new List<Review>();
            foreach(var rev in reviewList)
            {
                if(rev.ProductId == productId)
                {
                    reviewsForProduct.Add(rev);
                }
            }
            return View(reviewsForProduct);
        }

        //GET: Product/Create
        public async Task<IActionResult> Create(int productId)
        {
            Product product = _context.Products
                .SingleOrDefault(p => p.ProductId == productId);
            ViewData["ProductName"] = new SelectList(_context.Products, "ProductId", "Name");
            return View(product);
        }

        //POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(int productId, [Bind("ReviewId, Author, Content_Body, rating, ProductId")] Review review)
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", review.ProductId);
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Products");
        }
    }
}
