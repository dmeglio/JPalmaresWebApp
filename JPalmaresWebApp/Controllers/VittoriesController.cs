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
            return View(await _context.Vittorie.ToListAsync());
        }

        // GET: Vittories/Details/5
        public async Task<IActionResult> Details(long? id)
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
