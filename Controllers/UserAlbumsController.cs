using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Controllers
{
    public class UserAlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAlbumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAlbums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserAlbums.Include(u => u.Album).Include(u => u.SpotifyUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserAlbums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserAlbums == null)
            {
                return NotFound();
            }

            var userAlbum = await _context.UserAlbums
                .Include(u => u.Album)
                .Include(u => u.SpotifyUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAlbum == null)
            {
                return NotFound();
            }

            return View(userAlbum);
        }

        // GET: UserAlbums/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.SpotifyUsers, "Id", "Id");
            return View();
        }

        // POST: UserAlbums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,AlbumId")] UserAlbum userAlbum)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(userAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", userAlbum.AlbumId);
            ViewData["UserId"] = new SelectList(_context.SpotifyUsers, "Id", "Id", userAlbum.UserId);
            return View(userAlbum);
        }

        // GET: UserAlbums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserAlbums == null)
            {
                return NotFound();
            }

            var userAlbum = await _context.UserAlbums.FindAsync(id);
            if (userAlbum == null)
            {
                return NotFound();
            }

            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", userAlbum.AlbumId);
            ViewData["UserId"] = new SelectList(_context.SpotifyUsers, "Id", "Id", userAlbum.UserId);
            return View(userAlbum);
        }

        // POST: UserAlbums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,AlbumId")] UserAlbum userAlbum)
        {
            if (id != userAlbum.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAlbumExists(userAlbum.Id))
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

            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", userAlbum.AlbumId);
            ViewData["UserId"] = new SelectList(_context.SpotifyUsers, "Id", "Id", userAlbum.UserId);
            return View(userAlbum);
        }

        // GET: UserAlbums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserAlbums == null)
            {
                return NotFound();
            }

            var userAlbum = await _context.UserAlbums
                .Include(u => u.Album)
                .Include(u => u.SpotifyUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAlbum == null)
            {
                return NotFound();
            }

            return View(userAlbum);
        }

        // POST: UserAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserAlbums == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserAlbums'  is null.");
            }

            var userAlbum = await _context.UserAlbums.FindAsync(id);
            if (userAlbum != null)
            {
                _context.UserAlbums.Remove(userAlbum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAlbumExists(int id)
        {
            return (_context.UserAlbums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}