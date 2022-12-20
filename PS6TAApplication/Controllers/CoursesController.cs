/*
  Author:    Matt McFarlane
  Date:      12 / 19 / 2022
  Course:    CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and McFarlane - This work may not be copied for use in Academic Coursework.

  I, Matt McFarlane, certify that I wrote this code from scratch and did not copy it in part or whole from
  another source.  Any references used in the completion of the assignment are cited in my README file.

  File Contents

    Controller for managing Course objects.
    Why is the 'Modified By'/'Created By' stuff always broken with the CRUD stuff? It seems like I have
    to go in and manually change it so that it can pass the 'ModelState.IsValid' check every time.
*/
    
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PS6TAApplication.Areas.Data;
using PS6TAApplication.Data;
using PS6TAApplication.Models;

namespace PS6TAApplication.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        [Authorize(Roles = "Admin, Professor")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Course.ToListAsync());
        }

        // GET: Courses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SemesterOffered,YearOffered,Title,Department,Number,Section,Description,ProfessorUNID,ProfessorName,Schedule,Location,CreditHours,Enrollment,AdminNote")] Course course)
        {
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SemesterOffered,YearOffered,Title,Department,Number,Section,Description,ProfessorUNID,ProfessorName,Schedule,Location,CreditHours,Enrollment,AdminNote")] Course course)
        {
            if (id != course.ID)
            {
                return NotFound();
            }

            var courseOrig = _context.Course.Where(x => x.ID == id).FirstOrDefault();
            courseOrig.AdminNote = course.AdminNote;
            courseOrig.Schedule = course.Schedule;
            courseOrig.Number = course.Number;
            courseOrig.ProfessorName = course.ProfessorName;
            courseOrig.ProfessorUNID = course.ProfessorUNID;
            courseOrig.CreditHours = course.CreditHours;
            courseOrig.YearOffered = course.YearOffered;
            courseOrig.SemesterOffered = course.SemesterOffered;
            courseOrig.Department = course.Department;
            courseOrig.Description = course.Description;
            courseOrig.Enrollment = course.Enrollment;
            courseOrig.Location = course.Location;
            courseOrig.Section = course.Section;
            courseOrig.Title = course.Title;


            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { courseOrig.ID });
            }
            return View(courseOrig);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return _context.Course.Any(e => e.ID == id);
        }

        //Updates the Admin Note for a specific course
        [HttpPost]
        public async Task<string> updateNote(string id, string note)
        {
            var course = await _context.Course.FindAsync(int.Parse(id));
            if (course != null)
            {
                course.AdminNote = note;
                _context.Course.Update(course);
                await _context.SaveChangesAsync();
            }

            return $"data in cs: {id}, {note}";
        }
    }
}
