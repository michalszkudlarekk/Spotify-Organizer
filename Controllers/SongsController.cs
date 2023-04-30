using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;
using SpotifyOrganizer.Services;

namespace SpotifyOrganizer.Controllers;

public class SongsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SongsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Songs
    public async Task<IActionResult> Index()
    {
        return _context.Songs != null
            ? View(await _context.Songs.ToListAsync())
            : Problem("Entity set 'ApplicationDbContext.Songs'  is null.");
    }

    // GET: Songs/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Songs == null) return NotFound();

        var song = await _context.Songs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (song == null) return NotFound();

        return View(song);
    }

    // GET: Songs/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Songs/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SongName")] Song searchSong)
    {
        var trackName = searchSong.SongName;
        var spotifyApiService = new SpotifyApiService();
        var track = await spotifyApiService.SearchTrack(trackName);

        if (track == null) return NotFound();
        var song = new Song
        {
            SpotifyId = track.Id,
            SongName = track.Name,
            Artist = track.Artists[0].Name,
            ReleaseDate = track.Album.ReleaseDate
        };


        if (ModelState.IsValid) return View(song);
        _context.Add(song);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    // GET: Songs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Songs == null) return NotFound();

        var song = await _context.Songs.FindAsync(id);
        if (song == null) return NotFound();

        return View(song);
    }

    // POST: Songs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,SpotifyId,SongName,Artist,ReleaseDate,AddDate")]
        Song song)
    {
        if (id != song.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            try
            {
                _context.Update(song);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(song.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(song);
    }

    // GET: Songs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Songs == null) return NotFound();

        var song = await _context.Songs
            .FirstOrDefaultAsync(m => m.Id == id);
        if (song == null) return NotFound();

        return View(song);
    }

    // POST: Songs/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Songs == null) return Problem("Entity set 'ApplicationDbContext.Songs'  is null.");

        var song = await _context.Songs.FindAsync(id);
        if (song != null) _context.Songs.Remove(song);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SongExists(int id)
    {
        return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}