using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeAdmission.Data;
using CollegeAdmission.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace CollegeAdmission.Controllers
{
        public class CourseController : Controller
        {
            private readonly CollegeAdmissionContext _context;

            public CourseController(CollegeAdmissionContext context)
            {
                _context = context;
            }

            // GET: Course
            public async Task<IActionResult> Index()
            {
            if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("Name") != null)
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("Name");
                var login = _context.Login.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();

                if (name == "Admin@admin.com")
                {
                    return View(await _context.Course.ToListAsync());
                }
                else
                    return RedirectToAction(nameof(UserCourse));
            }
                else
                    return RedirectToAction(nameof(UserCourse));
            }
        
                public async Task<IActionResult> UserCourse()
                {
                
                    return View(await _context.Course.ToListAsync());
            }

            // GET: Course/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Course = await _context.Course
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Course == null)
                {
                    return NotFound();
                }

                return View(Course);
            }

            // GET: Course/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Course/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,CourseName,Grade,Duration")] Course Course)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Course);
            }

            // GET: Course/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Course = await _context.Course.FindAsync(id);
                if (Course == null)
                {
                    return NotFound();
                }
                return View(Course);
            }

            // POST: Course/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,Grade,Duration")] Course Course)
            {
                if (id != Course.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Course);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CourseExists(Course.Id))
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
                return View(Course);
            }

            // GET: Course/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Course = await _context.Course
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Course == null)
                {
                    return NotFound();
                }

                return View(Course);
            }

            // POST: Course/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Course = await _context.Course.FindAsync(id);
                _context.Course.Remove(Course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool CourseExists(int id)
            {
                return _context.Course.Any(e => e.Id == id);
            }
        }
    }