using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Recharge_WebApp.Data;
using Online_Recharge_WebApp.Models;

namespace Online_Recharge_WebApp.Controllers
{
    public class CustomerSupportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerSupportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerSupport
        public async Task<IActionResult> Index()
        {
              return _context.CustomerSupport != null ? 
                          View(await _context.CustomerSupport.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }

        // GET: CustomerSupport/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerSupport == null)
            {
                return NotFound();
            }

            var customerSupport = await _context.CustomerSupport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerSupport == null)
            {
                return NotFound();
            }

            return View(customerSupport);
        }

        // GET: CustomerSupport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerSupport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmailId,EntryType,TimeStamp,ComplaintMessage")] CustomerSupport customerSupport)
        {
            if (ModelState.IsValid && CustomerExists(customerSupport.EmailId.ToLower()))
            {
                _context.Add(customerSupport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerSupport);
        }

        // GET: CustomerSupport/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerSupport == null)
            {
                return NotFound();
            }
            ViewBag.Id=id;
            var customerSupport = await _context.CustomerSupport.FindAsync(id);
            if (customerSupport == null)
            {
                return NotFound();
            }

            return _context.CustomerSupport != null ?
                          View(await _context.CustomerSupport.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }

        // POST: CustomerSupport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string EmailId,string EntryType,string ComplaintMessage)
        {
            var customerSupport = await _context.CustomerSupport.FindAsync(id);
            if (id != customerSupport.Id)
            {
                return NotFound();
            }
            customerSupport.EmailId=EmailId;
            customerSupport.EntryType=EntryType;
            customerSupport.ComplaintMessage= ComplaintMessage;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerSupport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerSupportExists(customerSupport.Id))
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
            return _context.CustomerSupport != null ?
                          View(await _context.CustomerSupport.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }

      
        

      
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.CustomerSupport == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
            }
            var customerSupport = await _context.CustomerSupport.FindAsync(id);
            if (customerSupport != null)
            {
                _context.CustomerSupport.Remove(customerSupport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerSupportExists(int id)
        {
          return (_context.CustomerSupport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool CustomerExists(string id)
        {
          return (_context.Users?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
