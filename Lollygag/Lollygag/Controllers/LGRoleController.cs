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
    public class LGRoleController : Controller
    {
        private readonly LollygagContext _context;

        public LGRoleController(LollygagContext context)
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

        // GET: LGRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.LGRole.ToListAsync());
        }

        // GET: LGRoles/Details/5
        public async Task<IActionResult> Details(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGRole = await _context.LGRole
                .SingleOrDefaultAsync(m => m.RoleID == id);
            if (LGRole == null)
            {
                return NotFound();
            }

            return View(LGRole);
        }

        // GET: LGRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LGRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleI, RoleName, IsActive")] LGRole LGRole)
        {
            if (ModelState.IsValid)
            {
                LGRole.RoleID = GetRandomNumber(1, 10000000);
                _context.Add(LGRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(LGRole);
        }

        // GET: LGRoles/Edit/5
        public async Task<IActionResult> Edit(Int64? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LGRole = await _context.LGRole.SingleOrDefaultAsync(m => m.RoleID == id);
            if (LGRole == null)
            {
                return NotFound();
            }
            return View(LGRole);
        }

        // POST: LGRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int64 RoleID, [Bind("RoleI, RoleName, IsActive")] LGRole LGRole)
        {
            if (RoleID != LGRole.RoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(LGRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LGRoleExists(LGRole.RoleID))
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
            return View(LGRole);
        }

        // GET: LGRoles/Delete/5
        public async Task<IActionResult> Delete(Int64? RoleID)
        {
            if (RoleID == null)
            {
                return NotFound();
            }

            var LGRole = await _context.LGRole
                .SingleOrDefaultAsync(m => m.RoleID == RoleID);
            if (LGRole == null)
            {
                return NotFound();
            }

            return View(LGRole);
        }

        // POST: LGRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int64 RoleID)
        {
            var LGRole = await _context.LGRole.SingleOrDefaultAsync(m => m.RoleID == RoleID);
            _context.LGRole.Remove(LGRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LGRoleExists(Int64 RoleID)
        {
            return _context.LGRole.Any(e => e.RoleID == RoleID);
        }
    }
}
