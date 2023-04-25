using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Search.Video.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Online_Recharge_WebApp.Data;
using Online_Recharge_WebApp.Models;

namespace Online_Recharge_WebApp.Controllers
{
    [Authorize]
    public class CustomerSupportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CustomerSupportController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {
            return _context.CustomerSupport != null ?
                          View(await _context.CustomerSupport.Where(m => m.EmailId == User.Identity.Name).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }
        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        public IActionResult Create()
        {
            CustomerSupport customerSupport = new CustomerSupport();
            return View(customerSupport);
        }

        // POST: CustomerSupport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerSupport customerSupport)
        {
            customerSupport.EmailId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                _context.Add(customerSupport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerSupport);
        }
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerSupport == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            var customerSupport = await _context.CustomerSupport.FindAsync(id);
            if (customerSupport == null)
            {
                return NotFound();
            }

            return _context.CustomerSupport != null ?
                          View(await _context.CustomerSupport.Where(m => m.EmailId == User.Identity.Name).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }

        // POST: CustomerSupport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string EmailId, string EntryType, string ComplaintMessage)
        {
            var customerSupport = await _context.CustomerSupport.FindAsync(id);
            if (id != customerSupport.Id)
            {
                return NotFound();
            }
            customerSupport.EmailId = EmailId;
            customerSupport.EntryType = EntryType;
            customerSupport.ComplaintMessage = ComplaintMessage;

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
                          View(await _context.CustomerSupport.Where(m => m.EmailId == User.Identity.Name).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }
        [Authorize(Roles ="Customer")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisplayComplaints()
        {
            return _context.CustomerSupport != null ?
                          View(await _context.CustomerSupport.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
     
            Console.WriteLine("Inside Approve");

            var customerSupport = await _context.CustomerSupport.FindAsync(id);
            if (id != customerSupport.Id)
            {
                Console.WriteLine("NotFound");
                return NotFound();
            }

            customerSupport.Completed = !(customerSupport.Completed);
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("inside try");
                    _context.Update(customerSupport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("inside Cattch");
                    if (!CustomerSupportExists(customerSupport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DisplayComplaints));
            }

            return _context.CustomerSupport != null ?
                          View(await _context.CustomerSupport.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CustomerSupport'  is null.");
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
