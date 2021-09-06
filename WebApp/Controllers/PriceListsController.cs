using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WebApp.Database.Entity;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PriceListsController : Controller
    {
        private readonly DatabaseContext _context;

        public PriceListsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: PriceLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.PriceLists.ToListAsync());
        }

        // GET: PriceLists/Details/5
        public async Task<IActionResult> Details(int id=1)
        {

            if (id == null)
            {
                return NotFound();
            }

            var priceList = await _context.PriceLists.Include(t=>t.Columns).Include(t=>t.Products).ThenInclude(t=>t.Values)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        // GET: PriceLists/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateWithColumn()
        {
            return View("CreateWithColumn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithColumn(CreatePriceListWithColumn priceList)
        {
            if (ModelState.IsValid)
            {
                PriceList newPriceList = new PriceList() { Title = Request.Form["Title"] };
                
                var rows = (Request.Form.Keys.Count - 1) / 3;
                List<Column> columns = new List<Column>();
                for (int i = 0; i < rows; i++)
                {
                    Column column = new Column
                    {
                        Titile = Request.Form["Title[" + i + "]"],
                        TypeId = (ColumnTypeEnum)Convert.ToInt32(Request.Form["TypeId[" + i + "]"])

                    };
                    newPriceList.Columns.Add(column);
                  
                }
                _context.Add(newPriceList);


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceList);
        }
        // POST: PriceLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] PriceList priceList)
        {

            if (ModelState.IsValid)
            {
                _context.Add(priceList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceList);
        }

        // GET: PriceLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = await _context.PriceLists.Include(t=>t.Columns).FirstOrDefaultAsync(t=>t.Id == id.Value);
            if (priceList == null)
            {
                return NotFound();
            }

            PriceListModel priceListModel = new PriceListModel()
            {
                Id = priceList.Id,
                Title = priceList.Title,
                Columns = priceList.Columns?.ToList()
            };

            return View(priceListModel);
        }

        // POST: PriceLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] PriceListModel priceListModel)
        {
            if (id != priceListModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    PriceList priceList1 = await _context.PriceLists.Include(c => c.Columns).FirstOrDefaultAsync(t => t.Id == priceListModel.Id);
                    priceList1.Title = priceListModel.Title;
                    foreach(var col in priceList1.Columns)
                    {
                        if (priceListModel.Columns.Any(t => t.Id == col.Id))
                        {
                            var updateCol = priceListModel.Columns.FirstOrDefault(t => t.Id == col.Id);
                            col.Titile = updateCol.Titile;
                            col.TypeId = updateCol.TypeId;
                        }
                        else
                        {
                            priceList1.Columns.Remove(col);
                        }
                    }
                    foreach(var newCol in priceListModel.Columns.Where(t=>t.Id == 0))
                    {
                        priceList1.Columns.Add(newCol);
                    }

                    _context.Update(priceList1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceListExists(priceListModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(priceListModel);
        }

        // GET: PriceLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceList = await _context.PriceLists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        // POST: PriceLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
            var priceList = await _context.PriceLists.Include(t=>t.Columns).Include(t=>t.Products).ThenInclude(t=>t.Values).FirstOrDefaultAsync(t=>t.Id==id);
            _context.PriceLists.Remove(priceList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool PriceListExists(int id)
        {
            return _context.PriceLists.Any(e => e.Id == id);
        }
    }
}
