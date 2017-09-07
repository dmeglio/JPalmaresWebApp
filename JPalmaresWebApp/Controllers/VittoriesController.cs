using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPalmaresWebApp.Models;

namespace JPalmaresWebApp.Controllers
{
    public class VittoriesController : Controller
    {
        private readonly JPalmaresWebAppContext _context;

        public VittoriesController(JPalmaresWebAppContext context)
        {
            _context = context;
        }

        // GET: Vittories
        public async Task<IActionResult> Index()
        {
            var vittorie = await _context.Vittorie
                .Include(s => s.Trofei)
                .OrderBy(s => s.Stagione)
                .ToListAsync();

            return View(vittorie);
        }

        // GET: Vittories
        public async Task<IActionResult> Welcome(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            // Sorting
            ViewData["CurrentSort"] = sortOrder;
            ViewData["AllenatoreSortParm"] = sortOrder == "allenatore" ? "allenatore_desc" : "allenatore";
            ViewData["TrofeoSortParm"] = sortOrder == "trofeo" ? "trofeo_desc" : "trofeo";
            ViewData["StagioneSortParm"] = String.IsNullOrEmpty(sortOrder) ? "stagione_desc" : "";

            //Filtering
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            // Query
            var vittorie = from m in _context.Vittorie
                            .Include(s => s.Trofei)
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                vittorie = vittorie.Where(s => s.Allenatore.Contains(searchString)
                                       || s.Stagione.Contains(searchString)
                                       || s.Trofei.Trofeo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "allenatore_desc":
                    vittorie = vittorie.OrderByDescending(s => s.Allenatore);
                    break;
                case "allenatore":
                    vittorie = vittorie.OrderBy(s => s.Allenatore);
                    break;
                case "trofeo_desc":
                    vittorie = vittorie.OrderByDescending(s => s.Trofei.Trofeo);
                    break;
                case "trofeo":
                    vittorie = vittorie.OrderBy(s => s.Trofei.Trofeo);
                    break;
                case "stagione_desc":
                    vittorie = vittorie.OrderByDescending(s => s.Stagione);
                    break;
                default:
                    vittorie = vittorie.OrderBy(s => s.Stagione);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Vittorie>.CreateAsync(vittorie.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Vittories/Details/5
        public async Task<IActionResult> Partite(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vittorie = await _context.Vittorie
                .Include(s => s.Trofei)
                .Include(s => s.Partites)
                    .ThenInclude(s1 => s1.Squadre1)
                .Include(s => s.Partites)
                    .ThenInclude(s2 => s2.Squadre2)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (vittorie == null)
            {
                return NotFound();
            }

            return View(vittorie);
        }

        // GET: Vittories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vittorie = await _context.Vittorie
                .Include(s => s.Trofei)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (vittorie == null)
            {
                return NotFound();
            }

            return View(vittorie);
        }

        // GET: Vittories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vittories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idtrofeo,Stagione,Commenti,Ordstagione,Ordprogressivo,Allenatore")] Vittorie vittorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vittorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vittorie);
        }

        // GET: Vittories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vittorie = await _context.Vittorie.SingleOrDefaultAsync(m => m.Id == id);
            if (vittorie == null)
            {
                return NotFound();
            }
            return View(vittorie);
        }

        // POST: Vittories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Idtrofeo,Stagione,Commenti,Ordstagione,Ordprogressivo,Allenatore")] Vittorie vittorie)
        {
            if (id != vittorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vittorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VittorieExists(vittorie.Id))
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
            return View(vittorie);
        }

        // GET: Vittories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vittorie = await _context.Vittorie
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vittorie == null)
            {
                return NotFound();
            }

            return View(vittorie);
        }

        // POST: Vittories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var vittorie = await _context.Vittorie.SingleOrDefaultAsync(m => m.Id == id);
            _context.Vittorie.Remove(vittorie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VittorieExists(long id)
        {
            return _context.Vittorie.Any(e => e.Id == id);
        }
    }
}
