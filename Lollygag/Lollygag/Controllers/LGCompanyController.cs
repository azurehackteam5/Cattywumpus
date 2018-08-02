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
    public class LGCompanyController : Controller
    {
        private readonly LollygagContext _context;

        public LGCompanyController(LollygagContext context)
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

        // GET: LGCompanys
        public async Task<IActionResult> Index()
        {
            return View(await _context.LGCompany.ToListAsync());
        }

        // GET: LGCompanys/Details/5
        public async Task<IActionResult> Details(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGCompany = await _context.LGCompany
                .SingleOrDefaultAsync(m => m.CompanyID == id);
            if (LGCompany == null)
            {
                return NotFound();
            }

            return View(LGCompany);
        }

        // GET: LGCompanys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LGCompanys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyID, CompanyName, IsActive")] LGCompany LGCompany)
        {
            if (ModelState.IsValid)
            {
                LGCompany.CompanyID = GetRandomNumber(1, 10000000);
                _context.Add(LGCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(LGCompany);
        }

        // GET: LGCompanys/Edit/5
        public async Task<IActionResult> Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGCompany = await _context.LGCompany.SingleOrDefaultAsync(m => m.CompanyID == id);
            if (LGCompany == null)
            {
                return NotFound();
            }
            return View(LGCompany);
        }

        // POST: LGCompanys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int64 CompanyID, [Bind("CompanyID, CompanyName, IsActive")] LGCompany LGCompany)
        {
            if (CompanyID != LGCompany.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(LGCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LGCompanyExists(LGCompany.CompanyID))
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
            return View(LGCompany);
        }

        // GET: LGCompanys/Delete/5
        public async Task<IActionResult> Delete(Int64? CompanyID)
        {
            if (CompanyID == null)
            {
                return NotFound();
            }

            var LGCompany = await _context.LGCompany
                .SingleOrDefaultAsync(m => m.CompanyID == CompanyID);
            if (LGCompany == null)
            {
                return NotFound();
            }

            return View(LGCompany);
        }

        // POST: LGCompanys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int64 CompanyID)
        {
            var LGCompany = await _context.LGCompany.SingleOrDefaultAsync(m => m.CompanyID == CompanyID);
            _context.LGCompany.Remove(LGCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LGCompanyExists(Int64 CompanyID)
        {
            return _context.LGCompany.Any(e => e.CompanyID == CompanyID);
        }
    }
}
