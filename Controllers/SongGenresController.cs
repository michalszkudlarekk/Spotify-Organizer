using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Controllers
{
    public class SongGenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SongGenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SongGenres
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SongGenres.Include(s => s.Genre).Include(s => s.Song);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SongGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SongGenres == null)
            {
                return NotFound();
            }

            var songGenre = await _context.SongGenres
                .Include(s => s.Genre)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songGenre == null)
            {
                return NotFound();
            }

            return View(songGenre);
        }

        // GET: SongGenres/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id");
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id");
            return View();
        }

        // POST: SongGenres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,GenreId")] SongGenre songGenre)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(songGenre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", songGenre.GenreId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", songGenre.SongId);
            return View(songGenre);
        }

        // GET: SongGenres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SongGenres == null)
            {
                return NotFound();
            }

            var songGenre = await _context.SongGenres.FindAsync(id);
            if (songGenre == null)
            {
                return NotFound();
            }

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", songGenre.GenreId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", songGenre.SongId);
            return View(songGenre);
        }

        // POST: SongGenres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,GenreId")] SongGenre songGenre)
        {
            if (id != songGenre.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(songGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongGenreExists(songGenre.Id))
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

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Id", songGenre.GenreId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", songGenre.SongId);
            return View(songGenre);
        }

        // GET: SongGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SongGenres == null)
            {
                return NotFound();
            }

            var songGenre = await _context.SongGenres
                .Include(s => s.Genre)
                .Include(s => s.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (songGenre == null)
            {
                return NotFound();
            }

            return View(songGenre);
        }

        // POST: SongGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SongGenres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SongGenres'  is null.");
            }

            var songGenre = await _context.SongGenres.FindAsync(id);
            if (songGenre != null)
            {
                _context.SongGenres.Remove(songGenre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongGenreExists(int id)
        {
            return (_context.SongGenres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}