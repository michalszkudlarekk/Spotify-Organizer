using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Controllers
{
    /// <summary>
    /// Class <c>AlbumSongsController</c> is responsible for handling group-related actions in the application.
    /// It depends on the ApplicationDbContext, which manages the database context.
    /// Contains the following action methods: Index, Details, Create, Edit, and Delete and handle their exceptions.
    /// It connects Song and Album tables in the database and functions as adding songs to albums. 
    /// </summary>
    public class AlbumSongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumSongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlbumSongs
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            
           // var applicationDbContext = _context.AlbumsSong.Include(a => a.Album).Include(a => a.Song);
           // return View(await applicationDbContext.ToListAsync());
           var albumSongs = from s in _context.AlbumsSong.Include(a => a.Album).Include(a => a.Song) select s;
           albumSongs = albumSongs.OrderByDescending(s => s.Album.AlbumName);
           if (!String.IsNullOrEmpty(searchString))
           {
               albumSongs = albumSongs.Where(s => s.Album.AlbumName.Contains(searchString)
                                                  || s.Song.SongName.Contains(searchString));
           }
           return View(await albumSongs.AsNoTracking().ToListAsync());
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
            ViewData["Album"] = new SelectList(_context.Albums, "Id", "AlbumName");
            ViewData["Song"] = new SelectList(_context.Songs, "Id", "SongName");
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
                var existingAlbumSong = await _context.AlbumsSong.FirstOrDefaultAsync(s => s.SongId==albumSong.SongId && s.AlbumId==albumSong.AlbumId);
                if(existingAlbumSong != null) return RedirectToAction(nameof(Index));
                _context.Add(albumSong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Album"] = new SelectList(_context.Albums, "Id", "AlbumName");
            ViewData["Song"] = new SelectList(_context.Songs, "Id", "SongName");
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