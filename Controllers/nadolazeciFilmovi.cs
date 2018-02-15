using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Priprema1.Data;
using Priprema1.Models;

namespace Priprema1.Controllers
{
    public class nadolazeciFilmovi : Controller
    {
        private readonly ApplicationDbContext _context;

        public nadolazeciFilmovi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: nadolazeciFilmovi
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Films.Include(f => f.Zanr);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: nadolazeciFilmovi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var films = await _context.Films
                .Include(f => f.Zanr)
                .SingleOrDefaultAsync(m => m.id == id);
            if (films == null)
            {
                return NotFound();
            }

            return View(films);
        }

        // GET: nadolazeciFilmovi/Create
        public IActionResult Create()
        {
            ViewData["Zanrid"] = new SelectList(_context.zanr, "id", "id");
            return View();
        }

        // POST: nadolazeciFilmovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Naziv,Godina,Zanrid")] Films films)
        {
            if (ModelState.IsValid)
            {
                _context.Add(films);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Zanrid"] = new SelectList(_context.zanr, "id", "id", films.Zanrid);
            return View(films);
        }

        // GET: nadolazeciFilmovi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var films = await _context.Films.SingleOrDefaultAsync(m => m.id == id);
            if (films == null)
            {
                return NotFound();
            }
            ViewData["Zanrid"] = new SelectList(_context.zanr, "id", "id", films.Zanrid);
            return View(films);
        }

        // POST: nadolazeciFilmovi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Naziv,Godina,Zanrid")] Films films)
        {
            if (id != films.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(films);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmsExists(films.id))
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
            ViewData["Zanrid"] = new SelectList(_context.zanr, "id", "id", films.Zanrid);
            return View(films);
        }

        // GET: nadolazeciFilmovi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var films = await _context.Films
                .Include(f => f.Zanr)
                .SingleOrDefaultAsync(m => m.id == id);
            if (films == null)
            {
                return NotFound();
            }

            return View(films);
        }

        // POST: nadolazeciFilmovi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var films = await _context.Films.SingleOrDefaultAsync(m => m.id == id);
            _context.Films.Remove(films);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmsExists(int id)
        {
            return _context.Films.Any(e => e.id == id);
        }
    }
}
