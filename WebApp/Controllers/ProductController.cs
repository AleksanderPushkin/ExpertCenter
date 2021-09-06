using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Database.Entity;

namespace WebApp.Models
{
    public class ProductController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Product/Create
        public IActionResult Create(int id)
        {
            
           var plId =  _context.PriceLists.Include(t=>t.Columns).FirstOrDefault(t=>t.Id==id);
            ProductModel productModel = new ProductModel { Columns = plId.Columns.ToList(), PriceListId= id  };
            foreach (var column in productModel.Columns)
            {
                Value value = new Value() { ColumnId = column.Id };
                productModel.Values.Add(value);
            }
            
            return View(productModel);
        }


        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm()] ProductModel productModel)
        {
           
            if (ModelState.IsValid)
            { 
                var product = new Database.Entity.Product {  PriceListId=productModel.PriceListId,   Title=productModel.Title, VendorCode= productModel.Title };
                product.Values = new List<Value>();
                productModel.Values.ForEach(t=>product.Values.Add(t));
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details),"PriceLists" , new { id = productModel.PriceListId });
            }
            return View(productModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Include(t => t.Values).FirstOrDefault(t => t.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var price = _context.PriceLists.Include(t => t.Columns).FirstOrDefault(t => t.Id == product.PriceListId);
            ProductModel productModel = new ProductModel { Values = product.Values.ToList(), Columns = price.Columns.ToList(), Title = product.Title, VendorCode = product.VendorCode, Id = product.Id, PriceListId = product.PriceListId };
          
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _context.Products.Include(t => t.Values).FirstOrDefault(t => t.Id == productModel.Id);
                    product.Title = productModel.Title;
                    product.VendorCode = productModel.VendorCode;
                    product.Values = productModel.Values;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), "PriceLists",new { id = productModel.PriceListId});
            }
            return View(productModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.Include(t=>t.Values).FirstOrDefaultAsync(t=>t.Id==id);
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
