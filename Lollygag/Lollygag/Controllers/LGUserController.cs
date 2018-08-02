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
    public class LGUserController : Controller
    {
        private readonly LollygagContext _context;

        public LGUserController(LollygagContext context)
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

        // GET: LGUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.LGUser.ToListAsync());
        }

        // GET: LGUsers/Details/5
        public async Task<IActionResult> Details(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGUser = await _context.LGUser
                .SingleOrDefaultAsync(m => m.UserID == id);
            if (LGUser == null)
            {
                return NotFound();
            }

            return View(LGUser);
        }

        // GET: LGUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LGUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID, CompanyID, EmailAddress, Password, FirstName, LastName, RoleID, IsActive, AuditDate, AuditUserID")] LGUser LGUser)
        {
            if (ModelState.IsValid)
            {
                LGUser.UserID = GetRandomNumber(1, 10000000);
                _context.Add(LGUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(LGUser);
        }

        // GET: LGUsers/Edit/5
        public async Task<IActionResult> Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGUser = await _context.LGUser.SingleOrDefaultAsync(m => m.UserID == id);
            if (LGUser == null)
            {
                return NotFound();
            }
            return View(LGUser);
        }

        // POST: LGUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int64 UserID, [Bind("UserID, CompanyID, EmailAddress, Password, FirstName, LastName, RoleID, IsActive, AuditDate, AuditUserID")] LGUser LGUser)
        {
            if (UserID != LGUser.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(LGUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LGUserExists(LGUser.UserID))
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
            return View(LGUser);
        }

        // GET: LGUsers/Delete/5
        public async Task<IActionResult> Delete(Int64? UserID)
        {
            if (UserID == null)
            {
                return NotFound();
            }

            var LGUser = await _context.LGUser
                .SingleOrDefaultAsync(m => m.UserID == UserID);
            if (LGUser == null)
            {
                return NotFound();
            }

            return View(LGUser);
        }

        // POST: LGUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int64 UserID)
        {
            var LGUser = await _context.LGUser.SingleOrDefaultAsync(m => m.UserID == UserID);
            _context.LGUser.Remove(LGUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LGUserExists(Int64 UserID)
        {
            return _context.LGUser.Any(e => e.UserID == UserID);
        }
    }
}
