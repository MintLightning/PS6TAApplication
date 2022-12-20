/**
 * Authors:   Matt McFarlane, Zach Post
 * Date:      10/19/2022
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and McFarlane/Post - This work may not be copied for use in Academic Coursework.
 *
 * I, Matt McFarlane, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  Any references used in the completion of the assignment are cited in my README file.
 * I, Zach Post, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  Any references used in the completion of the assignment are cited in my README file.
 *
 * File Contents
 *
 *    Controller for handling requests related to Applications, including creating, editing, and viewing them in a list.
 */

using System;
using System.Collections.Generic;
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
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<TAUser> _userManager;
        public ApplicationsController(ApplicationDbContext context, UserManager<TAUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Applications/List
        [Authorize(Roles = "Admin, Professor")]
        public async Task<IActionResult> List()
        {
              return View(await _context.Application.ToListAsync());
        }

        // GET: Applications
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Application.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.ID == id);

            if (application == null)
            {
                return NotFound();
            }

            if (application.TAUserId != _userManager.FindByNameAsync(User.Identity?.Name).Result.Id && User.IsInRole("Applicant"))
            {
                return RedirectToAction(actionName: "GoAway", controllerName: "Home");
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CurrentDegree,Department,UofUGPA,HoursRequested,IsAvailableWeekBefore,SemestersCompleted,Statement,TransferSchool,LinkedInURL,ResumeFileName")] Application application)
        {
            ModelState.Remove("TAUser");
            ModelState.Remove("TAUserId");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");

            application.TAUser = await _userManager.GetUserAsync(User);
            application.TAUserId = application.TAUser.Id;

            if (ModelState.IsValid)
            {
                _context.Add(application);
                
                await _context.SaveChangesAsync();
                application.TAUser.Application = application;
                application.TAUser.ApplicationId = application.ID;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { application.ID });
            }

            return View(application);
        }

        // GET Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CurrentDegree,Department,UofUGPA,HoursRequested,IsAvailableWeekBefore,SemestersCompleted,Statement,TransferSchool,LinkedInURL,ResumeFileName")] Application application)
        {

            ModelState.Remove("TAUser");
            ModelState.Remove("TAUserId");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");

            var app = _context.Application
                .Where(o=>o.ID==id).Include(o=>o.TAUser).FirstOrDefault();
            if(app==null)
            {
                return NotFound();
            }

            app.UofUGPA = application.UofUGPA;
            app.CurrentDegree = application.CurrentDegree;
            app.HoursRequested = application.HoursRequested;
            app.IsAvailableWeekBefore = application.IsAvailableWeekBefore;
            app.SemestersCompleted = application.SemestersCompleted;
            app.Statement = application.Statement;
            app.TransferSchool = application.TransferSchool;
            app.LinkedInURL = application.LinkedInURL;
            app.ResumeFileName = application.ResumeFileName;

            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { app.ID });
            }
            return View(app);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Application == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Application == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Application'  is null.");
            }
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
                _userManager.FindByIdAsync(application.TAUserId).Result.ApplicationId = 0;
            }
            
            await _context.SaveChangesAsync();
            if(User.IsInRole("Applicant"))
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return RedirectToAction(nameof(List));
        }

        private bool ApplicationExists(int id)
        {
          return _context.Application.Any(e => e.ID == id);
        }
    }
}
