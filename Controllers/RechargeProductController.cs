using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Recharge_WebApp.Data;
using Online_Recharge_WebApp.Models;

namespace Online_Recharge_WebApp.Controllers
{
    public class RechargeProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RechargeProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexGeneral()
        {
            return _context.RechargeProduct != null ?
                        View(await _context.RechargeProduct.OrderBy(x => x.Plan).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.RechargeProduct'  is null.");
        }
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> IndexCustomer()
        {
            return _context.RechargeProduct != null ?
                        View(await _context.RechargeProduct.OrderBy(x => x.Plan).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.RechargeProduct'  is null.");
        }
        [Authorize(Roles ="Admin")]
        // GET: RechargeProduct
        public async Task<IActionResult> Index()
        {
              return _context.RechargeProduct != null ? 
                          View(await _context.RechargeProduct.OrderBy(x=>x.Plan).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RechargeProduct'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: RechargeProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RechargeProduct == null)
            {
                return NotFound();
            }

            var rechargeProductModel = await _context.RechargeProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rechargeProductModel == null)
            {
                return NotFound();
            }

            return View(rechargeProductModel);
        }
        [Authorize(Roles = "Admin")]
        // GET: RechargeProduct/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RechargeProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Plan,Validity,Data")] RechargeProductModel rechargeProductModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechargeProductModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rechargeProductModel);
        }
        [Authorize(Roles = "Admin")]
        // GET: RechargeProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RechargeProduct == null)
            {
                return NotFound();
            }

            var rechargeProductModel = await _context.RechargeProduct.FindAsync(id);
            if (rechargeProductModel == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return _context.RechargeProduct != null ?
                          View(await _context.RechargeProduct.OrderBy(x => x.Plan).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.RechargeProduct'  is null."); 
        }

        // POST: RechargeProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Plan,Validity,Data")] RechargeProductModel rechargeProductModel)
        {
            if (id != rechargeProductModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargeProductModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargeProductModelExists(rechargeProductModel.Id))
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
            return View(rechargeProductModel);
        }

      
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.RechargeProduct == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RechargeProduct'  is null.");
            }
            var rechargeProductModel = await _context.RechargeProduct.FindAsync(id);
            if (rechargeProductModel != null)
            {
                _context.RechargeProduct.Remove(rechargeProductModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargeProductModelExists(int id)
        {
          return (_context.RechargeProduct?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
