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
    public class SquadresController : Controller
    {
        private readonly JPalmaresWebAppContext _context;

        public SquadresController(JPalmaresWebAppContext context)
        {
            _context = context;
        }

        // GET: Squadres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Squadre.ToListAsync());
        }

        // GET: Squadres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squadre = await _context.Squadre
                .SingleOrDefaultAsync(m => m.Id == id);
            if (squadre == null)
            {
                return NotFound();
            }

            return View(squadre);
        }

        // GET: Squadres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Squadres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Immagine")] Squadre squadre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(squadre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(squadre);
        }

        // GET: Squadres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squadre = await _context.Squadre.SingleOrDefaultAsync(m => m.Id == id);
            if (squadre == null)
            {
                return NotFound();
            }
            return View(squadre);
        }

        // POST: Squadres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,Immagine")] Squadre squadre)
        {
            if (id != squadre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(squadre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SquadreExists(squadre.Id))
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
            return View(squadre);
        }

        // GET: Squadres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var squadre = await _context.Squadre
                .SingleOrDefaultAsync(m => m.Id == id);
            if (squadre == null)
            {
                return NotFound();
            }

            return View(squadre);
        }

        // POST: Squadres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var squadre = await _context.Squadre.SingleOrDefaultAsync(m => m.Id == id);
            _context.Squadre.Remove(squadre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SquadreExists(long id)
        {
            return _context.Squadre.Any(e => e.Id == id);
        }
    }
}
