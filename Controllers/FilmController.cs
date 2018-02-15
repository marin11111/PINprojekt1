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

namespace Priprema1.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Film
        public async Task<IActionResult> Index(string trazi)
        {
            //LINQ, slično SQL
            var filmovi = from f in _context.Films
                          select f;
            //Provjeri je li traži postavljen
            if (!String.IsNullOrEmpty(trazi))
            {
                filmovi = filmovi.Where(f => f.Naziv.Contains(trazi)); //Drugi način LINQ
            }

            //var applicationDbContext = _context.Film.Include(f => f.Zanr);
            return View(filmovi.ToList());
        }

        // GET: Film/Details/5
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

        // GET: Film/Create
        public IActionResult Create()
        {
            ViewData["Zanrid"] = new SelectList(_context.zanr, "id", "id");
            return View();
        }

        // POST: Film/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Godina,ZanrId")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ZanrId"] = new SelectList(_context.Zanr, "Id", "Id", film.ZanrId);
            return View(film);
        }

        // GET: Film/Edit/5
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

        // POST: Film/Edit/5
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

        // GET: Film/Delete/5
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

        // POST: Film/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.SingleOrDefaultAsync(m => m.Id == id);
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FilmsExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }
    }
}
