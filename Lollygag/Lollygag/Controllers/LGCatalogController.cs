using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lollygag.Models;

namespace Lollygag.Controllers
{
    public class LGCatalogController : Controller
    {
        private readonly LollygagContext _context;

        public LGCatalogController(LollygagContext context)
        {
            _context = context;
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        // GET: LGCatalogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.LGCatalog.ToListAsync());
        }

        // GET: LGCatalogs/Details/5
        public async Task<IActionResult> Details(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGCatalog = await _context.LGCatalog
                .SingleOrDefaultAsync(m => m.CatalogID == id);
            if (LGCatalog == null)
            {
                return NotFound();
            }

            return View(LGCatalog);
        }

        // GET: LGCatalogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LGCatalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogID, CatalogName, CompanyID, IsActive")] LGCatalog LGCatalog)
        {
            if (ModelState.IsValid)
            {
                LGCatalog.CatalogID = GetRandomNumber(1, 10000000);
                _context.Add(LGCatalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(LGCatalog);
        }

        // GET: LGCatalogs/Edit/5
        public async Task<IActionResult> Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGCatalog = await _context.LGCatalog.SingleOrDefaultAsync(m => m.CatalogID == id);
            if (LGCatalog == null)
            {
                return NotFound();
            }
            return View(LGCatalog);
        }

        // POST: LGCatalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int64 CatalogID, [Bind("CatalogID, CatalogName, CompanyID, IsActive")] LGCatalog LGCatalog)
        {
            if (CatalogID != LGCatalog.CatalogID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(LGCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LGCatalogExists(LGCatalog.CatalogID))
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
            return View(LGCatalog);
        }

        // GET: LGCatalogs/Delete/5
        public async Task<IActionResult> Delete(Int64? CatalogID)
        {
            if (CatalogID == null)
            {
                return NotFound();
            }

            var LGCatalog = await _context.LGCatalog
                .SingleOrDefaultAsync(m => m.CatalogID == CatalogID);
            if (LGCatalog == null)
            {
                return NotFound();
            }

            return View(LGCatalog);
        }

        // POST: LGCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int64 CatalogID)
        {
            var LGCatalog = await _context.LGCatalog.SingleOrDefaultAsync(m => m.CatalogID == CatalogID);
            _context.LGCatalog.Remove(LGCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LGCatalogExists(Int64 CatalogID)
        {
            return _context.LGCatalog.Any(e => e.CatalogID == CatalogID);
        }
    }
}
