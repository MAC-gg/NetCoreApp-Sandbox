using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Data;
using NetCoreApp.Models;

namespace NetCoreApp.Controllers
{
    public class MatchController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MatchController(ApplicationDbContext db)
        {
            // pass in the DbContext defined in Data
            _db = db;
        }

        public IActionResult Index()
        {
            // use db object to select matches and send to view
            IEnumerable<Match> objMatchesList = _db.Matches;
            return View(objMatchesList);
        }

        // GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Match obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                // adds to summary only
                // ModelState.AddModelError("CustomError", "The Display Order cannot match the Match Name.");

                // adds to name and therefore summary as well
                ModelState.AddModelError("name", "The Display Order cannot match the Match Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Matches.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Match created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET - EDIT
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var matchFromDb = _db.Matches.Find(id);
            // var matchFromDbFirst = _db.Matches.FirstOrDefault(u => u.Id == id);
            // var matchFromDbSingle = _db.Matches.SingleOrDefault(u => u.Id == id);

            if(matchFromDb == null)
            {
                return NotFound();
            }

            return View(matchFromDb);
        }

        // POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Match obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // adds to summary only
                // ModelState.AddModelError("CustomError", "The Display Order cannot match the Match Name.");

                // adds to name and therefore summary as well
                ModelState.AddModelError("name", "The Display Order cannot match the Match Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Matches.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Match updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET - EDIT
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var matchFromDb = _db.Matches.Find(id);
            // var matchFromDbFirst = _db.Matches.FirstOrDefault(u => u.Id == id);
            // var matchFromDbSingle = _db.Matches.SingleOrDefault(u => u.Id == id);

            if (matchFromDb == null)
            {
                return NotFound();
            }

            return View(matchFromDb);
        }

        // POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var matchFromDb = _db.Matches.Find(id);

            if (matchFromDb == null)
            {
                return NotFound();
            }
            
            _db.Matches.Remove(matchFromDb);
            _db.SaveChanges();
            TempData["success"] = "Match deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
