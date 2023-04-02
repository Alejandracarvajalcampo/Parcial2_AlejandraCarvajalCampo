using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertDB.DAL;
using ConcertDB.DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ConcertDB.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }


        //// GET: Tickets
        public async Task<IActionResult> Index(String searchString)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'DatabaseContext.Tickets'  is null.");
            }
            var Tickets = from m in _context.Tickets
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            //   if (searchGuid != Guid.Empty)
            {
                if (searchString.Count() >= 32) {
                    Guid searchGuid = new Guid(searchString);
                    Tickets = Tickets.Where(s => s.Id.Equals(searchGuid));

                    if (Tickets.Count() == 0)
                    {
                        ViewData["ShowModal"] = "True";
                        
                        Tickets = from m in _context.Tickets
                                      select m;
                    }
                    else
                    {
                        if (Tickets.First().IsUsed) 
                        {
                            ViewData["MessageModal"] =
                                "Esta boleta esta siendo usada en la localidad : " + Tickets.First().EntranceGate + 
                                " Y se registro el ingreso en la fecha y hora siguiente : "  + Tickets.First().UseDate +
                                "para ver mejor el detalle presione al boton DETALLES ";

                        } 
                        else 
                        {
                            ViewData["MessageModal"] = " Esta boleta esta disponible presione al boton EDITAR "; }
                        

                    }
                }else
                {
                    ViewData["ShowModal"] = "True";
                }


            }

            return View(await Tickets.ToListAsync());
        }

        // GET: Tickets/Details/5}


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }


        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Tickets tickets)
        {
            if (id != tickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tickets.IsUsed = true;
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.Id))
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
            return View(tickets);
        }
        private bool TicketsExists(Guid id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
