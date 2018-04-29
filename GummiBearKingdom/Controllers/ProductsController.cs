﻿using System;
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

        public ViewResult Index()
        {
            return View(productRepo.Products.ToList());
        }

        public IActionResult Details(int id)
        {
            Product thisProduct = productRepo.Products
                .Include(p => p.Category)
                .Include(r => r.Reviews)
                .FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            productRepo.Save(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            productRepo.Edit(product);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(x => x.ProductId == id);
            productRepo.Remove(thisProduct);
            return RedirectToAction("Index");
        }
    }

    //End of MockDatabase setup




    //WHEN GOING LIVE - COMMENT OUT METHODS ABOVE - COMMENT BACK IN THE METHODS BELOW
    //WHEN TESTING - COMMENT OUT METHODS BELOW - COMMENT BACK IN THE METHODS ABOVE



    //private readonly GummiDbContext _context;

    //    public ProductsController(GummiDbContext context)
    //    {
    //        _context = context;
    //    }


    //    // GET: Product
    //    public async Task<IActionResult> Index()
    //    {
    //        var gummiDbContext = _context.Products
    //            .Include(p => p.Category)
    //            .Include(r => r.Reviews);
    //        return View(await gummiDbContext.ToListAsync());
    //    }

    //    //GET: Product/Create
    //    public IActionResult Create()
    //    {
    //        ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryId", "Name");
    //        return View();
    //    }

    //    //POST: Product/Create
    //    [HttpPost]
    //    public async Task<IActionResult> Create([Bind("ProductId, Name, Description, Price, CategoryId")] Product product)
    //    {
    //        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
    //        _context.Add(product);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction("Index");
    //    }

    //    //GET: Product/Details/id
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        var product = await _context.Products
    //            .Include(p => p.Category)
    //            .Include(r => r.Reviews)
    //            .SingleOrDefaultAsync(m => m.ProductId == id);
    //        return View(product);
    //    }

    //    //GET: Product/Delete/id
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var product = await _context.Products
    //            .Include(p => p.Category)
    //            .SingleOrDefaultAsync(m => m.ProductId == id);
    //        return View(product);
    //    }

    //    //POST: Product/Delete/id
    //    [HttpPost, ActionName("Delete")]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var product = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
    //        _context.Products.Remove(product);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction("Index");
    //    }

    //    //GET: Product/Edit/id
    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var product = await _context.Products
    //            .Include(p => p.Category)
    //            .SingleOrDefaultAsync(c => c.ProductId == id);
    //        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");

    //        return View(product);
    //    }

    //    //POST: Product/Edit/id
    //    [HttpPost]
    //    public async Task<IActionResult> Edit(int id, [Bind("ProductId, Name, Description, Price, CategoryId")] Product product)
    //    {
    //        _context.Update(product);
    //        await _context.SaveChangesAsync();
    //        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
    //        return RedirectToAction("Index");
    //    }

    //    //GET: Product/DeleteAll
    //    public async Task<IActionResult> DeleteAll()
    //    {
    //        var gummiDbContext = _context.Products.Include(p => p.Category);
    //        return View(await gummiDbContext.ToListAsync());
    //    }

    //    //POST: Product/Delete/id
    //    [HttpPost, ActionName("DeleteAll")]
    //    public async Task<IActionResult> DeleteAllConfirmed()
    //    {
    //        _context.Database.ExecuteSqlCommand("TRUNCATE TABLE products");
    //        _context.Database.ExecuteSqlCommand("TRUNCATE TABLE reviews");
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction("Index", "Home");
    //    }
    //}
}
