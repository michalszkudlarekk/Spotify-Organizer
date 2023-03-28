using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpotifyOrganizer.Data;
using SpotifyOrganizer.Models;

namespace SpotifyOrganizer.Controllers
{
    public class SpotifyUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpotifyUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SpotifyUsers
        public async Task<IActionResult> Index()
        {
              return _context.SpotifyUsers != null ? 
                          View(await _context.SpotifyUsers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SpotifyUsers'  is null.");
        }

        // GET: SpotifyUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SpotifyUsers == null)
            {
                return NotFound();
            }

            var spotifyUser = await _context.SpotifyUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spotifyUser == null)
            {
                return NotFound();
            }

            return View(spotifyUser);
        }

        // GET: SpotifyUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpotifyUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,UserId")] SpotifyUser spotifyUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spotifyUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spotifyUser);
        }

        // GET: SpotifyUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SpotifyUsers == null)
            {
                return NotFound();
            }

            var spotifyUser = await _context.SpotifyUsers.FindAsync(id);
            if (spotifyUser == null)
            {
                return NotFound();
            }
            return View(spotifyUser);
        }

        // POST: SpotifyUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,UserId")] SpotifyUser spotifyUser)
        {
            if (id != spotifyUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spotifyUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpotifyUserExists(spotifyUser.Id))
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
            return View(spotifyUser);
        }

        // GET: SpotifyUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SpotifyUsers == null)
            {
                return NotFound();
            }

            var spotifyUser = await _context.SpotifyUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (spotifyUser == null)
            {
                return NotFound();
            }

            return View(spotifyUser);
        }

        // POST: SpotifyUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SpotifyUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SpotifyUsers'  is null.");
            }
            var spotifyUser = await _context.SpotifyUsers.FindAsync(id);
            if (spotifyUser != null)
            {
                _context.SpotifyUsers.Remove(spotifyUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpotifyUserExists(int id)
        {
          return (_context.SpotifyUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
