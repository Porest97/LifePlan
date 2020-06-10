using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LifePlan.Data;
using LifePlan.Models.DataModels;
using Microsoft.AspNetCore.Identity;

namespace LifePlan.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var lifePlanContext = _context.Person
                .Include(p => p.Company)
                .Include(p => p.IdentityUser)
                .Include(p => p.PersonCategory);
            return View(await lifePlanContext.ToListAsync());
        }

        // GET: People with LastName search !
        public async Task<IActionResult> IndexSearch(string searchString)
        {
            var people = from n in _context.Person
                .Include(p => p.Company)
                .Include(p => p.IdentityUser)
                .Include(p => p.PersonCategory)

                         select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                people = people
                .Include(p => p.Company)
                .Include(p => p.IdentityUser)
                .Include(p => p.PersonCategory)
                .Where(p => p.LastName.Contains(searchString));

            }
            return View(await people.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Company)
                .Include(p => p.IdentityUser)
                .Include(p => p.PersonCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Set<Company>(), "Id", "CompanyName");
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Email");
            ViewData["PersonCategoryId"] = new SelectList(_context.Set<PersonCategory>(), "Id", "PersonCategoryName");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,FirstName,LastName,StreetAddress,ZipCode,City,Country,Ssn,PhoneNumber1,PhoneNumber2,Email,IdentityUserId,PersonCategoryId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexSearch));
            }
            ViewData["CompanyId"] = new SelectList(_context.Set<Company>(), "Id", "CompanyName", person.CompanyId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Email", person.IdentityUserId);
            ViewData["PersonCategoryId"] = new SelectList(_context.Set<PersonCategory>(), "Id", "PersonCategoryName", person.PersonCategoryId);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Set<Company>(), "Id", "CompanyName", person.CompanyId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Email", person.IdentityUserId);
            ViewData["PersonCategoryId"] = new SelectList(_context.Set<PersonCategory>(), "Id", "PersonCategoryName", person.PersonCategoryId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,FirstName,LastName,StreetAddress,ZipCode,City,Country,Ssn,PhoneNumber1,PhoneNumber2,Email,IdentityUserId,PersonCategoryId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexSearch));
            }
            ViewData["CompanyId"] = new SelectList(_context.Set<Company>(), "Id", "CompanyName", person.CompanyId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Email", person.IdentityUserId);
            ViewData["PersonCategoryId"] = new SelectList(_context.Set<PersonCategory>(), "Id", "PersonCategoryName", person.PersonCategoryId);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Company)
                .Include(p => p.IdentityUser)
                .Include(p => p.PersonCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSearch));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
