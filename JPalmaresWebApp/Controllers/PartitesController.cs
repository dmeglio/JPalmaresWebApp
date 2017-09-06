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
    public class PartitesController : Controller
    {
        private readonly JPalmaresWebAppContext _context;

        public PartitesController(JPalmaresWebAppContext context)
        {
            _context = context;
        }

        // GET: Partites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Partite.ToListAsync());
        }

        // GET: Partites/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partite = await _context.Partite
                .SingleOrDefaultAsync(m => m.Id == id);
            if (partite == null)
            {
                return NotFound();
            }

            return View(partite);
        }

        // GET: Partites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idvittoria,Idsquadra1,Idsquadra2,Luogo,Data,Tabellino,Identif,Dts,Dcr,Commento,Punteggio1,Punteggio2,Puntbcr1,Puntbcr2,Ordine")] Partite partite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partite);
        }

        // GET: Partites/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partite = await _context.Partite.SingleOrDefaultAsync(m => m.Id == id);
            if (partite == null)
            {
                return NotFound();
            }
            return View(partite);
        }

        // POST: Partites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Idvittoria,Idsquadra1,Idsquadra2,Luogo,Data,Tabellino,Identif,Dts,Dcr,Commento,Punteggio1,Punteggio2,Puntbcr1,Puntbcr2,Ordine")] Partite partite)
        {
            if (id != partite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartiteExists(partite.Id))
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
            return View(partite);
        }

        // GET: Partites/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partite = await _context.Partite
                .SingleOrDefaultAsync(m => m.Id == id);
            if (partite == null)
            {
                return NotFound();
            }

            return View(partite);
        }

        // POST: Partites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var partite = await _context.Partite.SingleOrDefaultAsync(m => m.Id == id);
            _context.Partite.Remove(partite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartiteExists(long id)
        {
            return _context.Partite.Any(e => e.Id == id);
        }
    }
}
