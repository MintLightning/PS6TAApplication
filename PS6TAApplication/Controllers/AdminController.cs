/**
 * Authors:   Matt McFarlane, Zach Post
 * Date:      10/1/2022
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
 *    Handles http requests for RoleManager page
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PS6TAApplication.Areas.Data;
using PS6TAApplication.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PS6TAApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<TAUser> _userManager;
        private readonly SignInManager<TAUser> _signInManager;

        //automagic imports
        public AdminController(ILogger<AdminController> logger, UserManager<TAUser> userManager, SignInManager<TAUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Serves the RoleManager page
        public IActionResult RoleManager()
        {
            return View();
        }

        //Toggles the specified role of user with given id on/off
        [HttpPost]
        public async Task<string> toggleRole(string id, string role)
        {
            TAUser user = await _userManager.FindByIdAsync(id);
            if(await _userManager.IsInRoleAsync(user, role))
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return $"data in cs: {id}, {role}";
        }
    }

}
