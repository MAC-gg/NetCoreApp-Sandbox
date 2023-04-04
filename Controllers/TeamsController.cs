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
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TeamsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
              return _db.Team != null ? 
                          View(await _db.Team.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Team'  is null.");
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Team == null)
            {
                return NotFound();
            }

            var team = await _db.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create(int id)
        {
            var leagueData = _db.League.Find(id);

            if (leagueData == null)
            {
                return NotFound();
            }

            var teams = _db.Team.Where(a => a.LeagueID == id).ToList();
            if (leagueData.numTeamsToStart == teams.Count)
            {
                TempData["error"] = "Sorry, this League is full.";
                return RedirectToAction("Details", "Leagues", new { id });
            }

            var VM = new CreateTeamVM {
                LeagueId=id,
                LeagueName=leagueData.Name
            };
            // var matchFromDbFirst = _db.Matches.FirstOrDefault(u => u.Id == id);
            // var matchFromDbSingle = _db.Matches.SingleOrDefault(u => u.Id == id);

            // ViewData["leagueData"] = leagueData;
            // ViewBag.leagueName = leagueData.Name;
            // ViewBag.leagueID = leagueData.Id;

            return View(VM);
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,CreatedDateTime,LeagueID,Name")] Team team)
        public IActionResult Create(CreateTeamVM VM)
        {
            if (ModelState.IsValid)
            {
                var TeamToAdd = new Team
                {
                    Name = VM.TeamName,
                    CreatedDateTime = DateTime.Now,
                    LeagueID = VM.LeagueId,
                };

                _db.Team.Add(TeamToAdd);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(VM);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Team == null)
            {
                return NotFound();
            }

            var team = await _db.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDateTime,LeagueID,Name")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(team);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Team == null)
            {
                return NotFound();
            }

            var team = await _db.Team
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Team == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Team'  is null.");
            }
            var team = await _db.Team.FindAsync(id);
            if (team != null)
            {
                _db.Team.Remove(team);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
          return (_db.Team?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
