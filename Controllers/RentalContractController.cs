using DriveNow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace DriveNow.Controllers
{
    public class RentalContractController : Controller
    {
        private readonly AppDbContext _context;

        public RentalContractController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var contracts = _context.RentalContracts
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .OrderByDescending(r => r.StartDate);

            var pagedContracts = contracts.ToPagedList(pageNumber, pageSize);

            return View(pagedContracts);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Vehicles = _context.Vehicles.Where(v => !v.IsRented).ToList();

            var rentalContract = new RentalContract
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };

            return View(rentalContract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalContract rentalContract)
        {
            ModelState.Remove("Customer");
            ModelState.Remove("Vehicle");

            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(_context.Customers, "Id", "FullName", rentalContract.CustomerId);
                ViewBag.Vehicles = new SelectList(_context.Vehicles.Where(v => !v.IsRented), "Id", "LicensePlate", rentalContract.VehicleId);
                return View(rentalContract);
            }

            var vehicle = await _context.Vehicles.FindAsync(rentalContract.VehicleId);
            if (vehicle != null)
            {
                vehicle.IsRented = true;
                _context.Vehicles.Update(vehicle);
            }

            _context.RentalContracts.Add(rentalContract);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rentalContract = await _context.RentalContracts
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rentalContract == null) return NotFound();

            return View(rentalContract);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rentalContract = await _context.RentalContracts
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rentalContract == null) return NotFound();

            return View(rentalContract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RentalContract rentalContract)
        {
            if (id != rentalContract.Id)
                return NotFound();

            ModelState.Remove("Customer");
            ModelState.Remove("Vehicle");

            if (!ModelState.IsValid)
            {
                var loadedContract = await _context.RentalContracts
                    .Include(r => r.Customer)
                    .Include(r => r.Vehicle)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (loadedContract == null)
                    return NotFound();

                return View(loadedContract);
            }

            var existingRentalContract = await _context.RentalContracts
                .AsNoTracking()
                .FirstOrDefaultAsync(rc => rc.Id == id);

            if (existingRentalContract == null)
                return NotFound();

            rentalContract.CustomerId = existingRentalContract.CustomerId;
            rentalContract.VehicleId = existingRentalContract.VehicleId;
            rentalContract.StartDate = existingRentalContract.StartDate;
            rentalContract.InitialMileage = existingRentalContract.InitialMileage;

            try
            {
                _context.Update(rentalContract);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RentalContracts.Any(e => e.Id == rentalContract.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rentalContract = await _context.RentalContracts
                .Include(r => r.Customer)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(rc => rc.Id == id);

            if (rentalContract == null) return NotFound();

            return View(rentalContract);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalContract = await _context.RentalContracts
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (rentalContract != null)
            {
                var vehicle = rentalContract.Vehicle;
                if (vehicle != null)
                {
                    vehicle.IsRented = false;
                    _context.Update(vehicle);
                }

                _context.RentalContracts.Remove(rentalContract);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RentalContractExists(int id)
        {
            return _context.RentalContracts.Any(e => e.Id == id);
        }
    }
}
