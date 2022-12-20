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
 *    TAUser object that makes up user database and is used for accounts on the site
 */
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using PS6TAApplication.Models;

namespace PS6TAApplication.Areas.Data
{
    [Index(nameof(Unid), IsUnique=true)]
    public class TAUser : IdentityUser
    {
        public string Unid { get; set; }

        public string Name { get; set; }

        public string ReferredTo { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
