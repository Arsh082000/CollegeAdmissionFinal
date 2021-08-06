using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeAdmission.Data;
using CollegeAdmission.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CollegeAdmission.Controllers
{
        public class RegistrationController : Controller
        {
            private readonly CollegeAdmissionContext _context;

            public RegistrationController(CollegeAdmissionContext context)
            {
                _context = context;
            }

            // GET: Registration
            public async Task<IActionResult> Index()
            {
            if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("Name") != null)
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("Name");
                var login = _context.Login.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();
                if(_context.Course.ToList().Count == 0)
                {
                    return RedirectToAction(nameof(CourseNotFound));
                }
                else
                    if(name == "Admin@admin.com")
                {
                    return View(await _context.Registration.ToListAsync());
                }
                else
                {
                    return View(await _context.Registration.Where(x => x.UserId == Convert.ToInt32(value)).ToListAsync());
                }

            }
            else
                return RedirectToAction("UserLogin", "Logins");
            }

            // GET: Registration/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Registration = await _context.Registration
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Registration == null)
                {
                    return NotFound();
                }

                return View(Registration);
            }
        public IActionResult CourseNotFound()
        {
            return View();
        }
        // GET: Registration/Create
        public IActionResult Create()
            {
            var courses = _context.Course.ToList();
            courses.Insert(0, new Course { Id = 0, CourseName = "Select Course" });
            ViewBag.ListCourses = courses;
            return View();
            }

            // POST: Registration/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,FatherName,Gender,Qualification,CourseId,Contact,Email,Address,UserId")] Registration Registration)
            {
                if (ModelState.IsValid)
                {
                    Registration.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Add(Registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Registration);
            }

            // GET: Registration/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Registration = await _context.Registration.FindAsync(id);
                if (Registration == null)
                {
                    return NotFound();
                }
                return View(Registration);
            }

            // POST: Registration/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FatherName,Gender,Qualification,CourseId,Contact,Email,Address")] Registration Registration)
            {
                if (id != Registration.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Registration);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RegistrationExists(Registration.Id))
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
                return View(Registration);
            }

            // GET: Registration/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Registration = await _context.Registration
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Registration == null)
                {
                    return NotFound();
                }

                return View(Registration);
            }

            // POST: Registration/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Registration = await _context.Registration.FindAsync(id);
                _context.Registration.Remove(Registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool RegistrationExists(int id)
            {
                return _context.Registration.Any(e => e.Id == id);
            }
        }
    }
