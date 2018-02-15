using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Priprema1.Data;
using Priprema1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Priprema1.Controllers
{
    [Authorize]
    public class zanrController : Controller
    {
        private readonly ApplicationDbContext _context;

        public zanrController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: zanr
        public async Task<IActionResult> Index()
        {
            return View(await _context.zanr.ToListAsync());
        }

        // GET: zanr/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.zanr
                .SingleOrDefaultAsync(m => m.id == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // GET: zanr/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: zanr/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Naziv")] zanr zanr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zanr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zanr);
        }

        // GET: zanr/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.zanr.SingleOrDefaultAsync(m => m.id == id);
            if (zanr == null)
            {
                return NotFound();
            }
            return View(zanr);
        }

        // POST: zanr/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Naziv")] zanr zanr)
        {
            if (id != zanr.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zanr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!zanrExists(zanr.id))
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
            return View(zanr);
        }

        // GET: zanr/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zanr = await _context.zanr
                .SingleOrDefaultAsync(m => m.id == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // POST: zanr/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zanr = await _context.zanr.SingleOrDefaultAsync(m => m.id == id);
            _context.zanr.Remove(zanr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool zanrExists(int id)
        {
            return _context.zanr.Any(e => e.id == id);
        }
    }
}
