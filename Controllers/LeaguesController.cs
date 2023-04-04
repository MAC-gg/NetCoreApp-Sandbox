using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreApp.Data;
using NetCoreApp.Models;
using NetCoreApp.ViewModels;

namespace NetCoreApp.Controllers
{
    public class LeaguesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LeaguesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Leagues
        public async Task<IActionResult> Index()
        {
            return _db.League != null ?
                        View(await _db.League.ToListAsync()) :
                          Problem("Entity set 'ApplicationDb_db.League'  is null.");
        }

        // GET: Leagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.League == null || _db.Team == null)
            {
                return NotFound();
            }

            var league = await _db.League
                .FirstOrDefaultAsync(m => m.Id == id);
            if (league == null)
            {
                return NotFound();
            }

            /* Get Teams */ 
            var teams = _db.Team.Where(a => a.LeagueID == id).ToList();
            // null check
            if (teams == null) { return NotFound(); }


            var VM = new DetailsLeagueVM
            {
                league = league,
                teams = teams
            };

            return View(VM);
        }

        // GET: Leagues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDateTime,Name")] League league)
        {
            if (ModelState.IsValid)
            {
                _db.Add(league);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(league);
        }

        // GET: Leagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.League == null)
            {
                return NotFound();
            }

            var league = await _db.League.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }
            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDateTime,Name")] League league)
        {
            if (id != league.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(league);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueExists(league.Id))
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
            return View(league);
        }

        // GET: Leagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.League == null)
            {
                return NotFound();
            }

            var league = await _db.League
                .FirstOrDefaultAsync(m => m.Id == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.League == null)
            {
                return Problem("Entity set 'ApplicationDbContext.League'  is null.");
            }
            var league = await _db.League.FindAsync(id);
            if (league != null)
            {
                _db.League.Remove(league);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueExists(int id)
        {
          return (_db.League?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
