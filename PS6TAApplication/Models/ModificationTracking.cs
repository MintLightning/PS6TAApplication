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
 *    Model for modification tracking, inherited by Application
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PS6TAApplication.Models
{
    public class ModificationTracking
    {
        // The time when the application was created
        [ScaffoldColumn(false)]
        public DateTime CreationDate { get; set; }

        // Will intially be the same as the creation, but then be updated whenever a change is made
        [ScaffoldColumn(false)]
        public DateTime ModificationDate { get; set; }

        // Name of user who created the row
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        // Name of last user to modify the data
        [ScaffoldColumn(false)]
        public string ModifiedBy { get; set; }
    }
}
