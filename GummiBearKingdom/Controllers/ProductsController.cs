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
        //Beginning of MockDatabase setup

        public IGummiRepository productRepo;

        public ProductsController(IGummiRepository repo = null)
        {
            if (repo == null)
            {
                this.productRepo = new EFProductRepository();
            }
            else
            {
                this.productRepo = repo;
            }
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var gummiDbContext = productRepo.Products
                .Include(p => p.Category)
                .Include(r => r.Reviews);
            return View(await gummiDbContext.ToListAsync());
        }

        //GET: Product/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            var product = await productRepo.Products
                .Include(p => p.Category)
                .Include(r => r.Reviews)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            return View(product);
        }

        //GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(productRepo.Categories, "CategoryId", "Name");
            return View();
        }

        //POST: Product/Create
        [HttpPost]
        public IActionResult Create([Bind("ProductId, Name, Description, Price, CategoryId")] Product product)
        {
            ViewData["CategoryId"] = new SelectList(productRepo.Categories, "CategoryId", "CategoryId", product.CategoryId);
            productRepo.Save(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        //GET: Product/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productRepo.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(c => c.ProductId == id);
            ViewData["CategoryId"] = new SelectList(productRepo.Categories, "CategoryId", "Name");

            return View(product);
        }

        //POST: Product/Edit/id
        [HttpPost]
        public IActionResult Edit(int id, [Bind("ProductId, Name, Description, Price, CategoryId")] Product product)
        {
            productRepo.Edit(product);
            ViewData["CategoryId"] = new SelectList(productRepo.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return RedirectToAction("Index");
        }

        //GET: Product/Delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var product = await productRepo.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.ProductId == id);
            return View(product);
        }

        //POST: Product/Delete/id
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await productRepo.Products.SingleOrDefaultAsync(p => p.ProductId == id);
            productRepo.Remove(product);
            return RedirectToAction("Index");
        }

        //GET: Product/DeleteAll
        public async Task<IActionResult> DeleteAll()
        {
            var gummiDbContext = productRepo.Products.Include(p => p.Category);
            return View(await gummiDbContext.ToListAsync());
        }
        //POST: Product/Delete/id
        [HttpPost, ActionName("DeleteAll")]
        public async Task<IActionResult> DeleteAllConfirmed()
        {
            productRepo.RemoveAll();
            return RedirectToAction("Index");
        } 
    }
}
