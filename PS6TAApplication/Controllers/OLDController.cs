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
 *    Controller for handling requests to old pages built for previous assignments
 */

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PS6TAApplication.Areas.Data;
using System.Data;

namespace PS6TAApplication.Controllers
{
    [Authorize]
    public class OLDController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<TAUser> _userManager;
        private readonly SignInManager<TAUser> _signInManager;

        //Automagic imports
        public OLDController(ILogger<HomeController> logger, UserManager<TAUser> userManager, SignInManager<TAUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Serves ApplicationList page, only visible to Admins and Professors
        public IActionResult ApplicationList()
        {
            return View();
        }
        //Serves ApplicationCreate page, only visible to Applicants
        public IActionResult ApplicationCreate()
        {
            return View();
        }

        //Serves ApplicationCreate page, only visible to Admins, Professors and u0000000, other users get redirected to the go away page
        public IActionResult ApplicationDetails()
        {
            return View();
        }
    }
}
