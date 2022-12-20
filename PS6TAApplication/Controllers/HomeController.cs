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
 *    Handles http requests for the "Home" section of the web page, which includes everything except for the RoleManager page
 */
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PS6TAApplication.Areas.Data;
using PS6TAApplication.Models;
using System.Diagnostics;

namespace PS6TAApplication.Controllers
{
    //Only logged in users are authorized
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<TAUser> _userManager;
        private readonly SignInManager<TAUser> _signInManager;

        //Automagic imports
        public HomeController(ILogger<HomeController> logger, UserManager<TAUser> userManager, SignInManager<TAUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Serves home page, allows non-logged in users
        [AllowAnonymous]
        public IActionResult Index()
        {
            
            return View();
        }


        //Serves privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        //Serves the go away page
        public IActionResult GoAway()
        {
            return View();
        }
        //Redirects to go away page
        public RedirectToActionResult RedirectGoAway()
        {
            return RedirectToAction(actionName: "GoAway", controllerName: "Home");
        }

        //Displays error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}