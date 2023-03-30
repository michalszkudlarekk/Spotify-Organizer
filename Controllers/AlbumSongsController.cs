using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Controllers
{
    public class AlbumSongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumSongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlbumSongs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AlbumsSong.Include(a => a.Album).Include(a => a.Song);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AlbumSongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AlbumsSong == null)
            {
                return NotFound();
            }

            var albumSong = await _context.AlbumsSong
                .Include(a => a.Album)
                .Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albumSong == null)
            {
                return NotFound();
            }

            return View(albumSong);
        }

        // GET: AlbumSongs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id");
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id");
            return View();
        }

        // POST: AlbumSongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SongId,AlbumId")] AlbumSong albumSong)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(albumSong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", albumSong.AlbumId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", albumSong.SongId);
            return View(albumSong);
        }

        // GET: AlbumSongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AlbumsSong == null)
            {
                return NotFound();
            }

            var albumSong = await _context.AlbumsSong.FindAsync(id);
            if (albumSong == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", albumSong.AlbumId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", albumSong.SongId);
            return View(albumSong);
        }

        // POST: AlbumSongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SongId,AlbumId")] AlbumSong albumSong)
        {
            if (id != albumSong.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(albumSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumSongExists(albumSong.Id))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", albumSong.AlbumId);
            ViewData["SongId"] = new SelectList(_context.Songs, "Id", "Id", albumSong.SongId);
            return View(albumSong);
        }

        // GET: AlbumSongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AlbumsSong == null)
            {
                return NotFound();
            }

            var albumSong = await _context.AlbumsSong
                .Include(a => a.Album)
                .Include(a => a.Song)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (albumSong == null)
            {
                return NotFound();
            }

            return View(albumSong);
        }

        // POST: AlbumSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AlbumsSong == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AlbumsSong'  is null.");
            }
            var albumSong = await _context.AlbumsSong.FindAsync(id);
            if (albumSong != null)
            {
                _context.AlbumsSong.Remove(albumSong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumSongExists(int id)
        {
            return (_context.AlbumsSong?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
