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
    public class TrofeisController : Controller
    {
        private readonly JPalmaresWebAppContext _context;

        public TrofeisController(JPalmaresWebAppContext context)
        {
            _context = context;
        }

        // GET: Trofeis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trofei.ToListAsync());
        }

        // GET: Trofeis/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trofei = await _context.Trofei
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trofei == null)
            {
                return NotFound();
            }

            return View(trofei);
        }

        // GET: Trofeis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trofeis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Trofeo,Immagine,Bigimmagine,Ufficiale,Antico,Amichevole,Importante,Ordprogressivo")] Trofei trofei)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trofei);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trofei);
        }

        // GET: Trofeis/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trofei = await _context.Trofei.SingleOrDefaultAsync(m => m.Id == id);
            if (trofei == null)
            {
                return NotFound();
            }
            return View(trofei);
        }

        // POST: Trofeis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Trofeo,Immagine,Bigimmagine,Ufficiale,Antico,Amichevole,Importante,Ordprogressivo")] Trofei trofei)
        {
            if (id != trofei.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trofei);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrofeiExists(trofei.Id))
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
            return View(trofei);
        }

        // GET: Trofeis/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trofei = await _context.Trofei
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trofei == null)
            {
                return NotFound();
            }

            return View(trofei);
        }

        // POST: Trofeis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var trofei = await _context.Trofei.SingleOrDefaultAsync(m => m.Id == id);
            _context.Trofei.Remove(trofei);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrofeiExists(long id)
        {
            return _context.Trofei.Any(e => e.Id == id);
        }
    }
}
