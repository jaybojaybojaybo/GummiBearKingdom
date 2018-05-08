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
        public IActionResult Index()
        {
            var gummiDbContext = productRepo.Products
                .Include(p => p.Category)
                .Include(r => r.Reviews);
            return View(gummiDbContext.ToList());
        }

        //GET: Product/Details/id
        public IActionResult Details(int? id)
        {
            var product = productRepo.Products
                .Include(p => p.Category)
                .Include(r => r.Reviews)
                .SingleOrDefault(m => m.ProductId == id);
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
        public IActionResult Create(Product product)
        {
            ViewData["CategoryId"] = new SelectList(productRepo.Categories, "CategoryId", "CategoryId", product.CategoryId);
            productRepo.Save(product);
            return RedirectToAction("Index");
        }

        //GET: Product/Edit/id
        public IActionResult Edit(int id)
        {
            var product = productRepo.Products
                .Include(p => p.Category)
                .SingleOrDefault(c => c.ProductId == id);
            ViewData["CategoryId"] = new SelectList(productRepo.Categories, "CategoryId", "Name");

            return View(product);
        }

        //POST: Product/Edit/id
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            productRepo.Edit(product);
            ViewData["CategoryId"] = new SelectList(productRepo.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return RedirectToAction("Index");
        }

        //GET: Product/Delete/id
        public IActionResult Delete(int id)
        {
            var product = productRepo.Products
                .Include(p => p.Category)
                .SingleOrDefault(m => m.ProductId == id);
            return View(product);
        }

        //POST: Product/Delete/id
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = productRepo.Products
                .Include(r => r.Reviews)
                .SingleOrDefault(p => p.ProductId == id);
            productRepo.Remove(product);
            return RedirectToAction("Index");
        }

        //GET: Product/DeleteAll
        public IActionResult DeleteAll()
        {
            var gummiDbContext = productRepo.Products.Include(p => p.Category);
            return View(gummiDbContext.ToList());
        }
        //POST: Product/Delete/id
        [HttpPost, ActionName("DeleteAll")]
        public IActionResult DeleteAllConfirmed()
        {
            productRepo.RemoveAll();
            return RedirectToAction("Index");
        } 
    }
}
