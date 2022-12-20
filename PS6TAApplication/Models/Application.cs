/**
 * Authors:   Matt McFarlane, Zach Post
 * Date:      10/15/2022
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
 *    Object structure representing an Application
 */

using PS6TAApplication.Areas.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace PS6TAApplication.Models
{
    public class Application : ModificationTracking
    {
        // The different degrees an applicant can choose from
        public enum degree
        {
            BS,
            Masters,
            BSMS,
            PhD
        }
        public int ID { get; set; }

        public string TAUserId { get; set; }
        public TAUser TAUser { get; set; } = null!;

        // The applicant's currently persued degree
        [Required]
        [Display(Name = "Current Degree", ShortName = "Degree", Description = "Enter the degree you are currently persuing")]
        public degree CurrentDegree { get; set; }

        // The department that the Applicant is currently enrolled in
        [Required]
        [Display(Name = "Department", ShortName = "Dpt.", Prompt = "CS, CE, ME, etc.", Description = "Enter the department for the degree you are currently persuing")]
        [DisplayFormat(NullDisplayText = "Type here...")]
        [StringLength(50)]
        public string Department { get; set; } = "";

        // The applicant's current GPA at UofU
        [Required]
        [Display(Name = "UofU GPA", ShortName = "GPA", Prompt = "0.000", Description = "Enter your current GPA at the University of Utah")]
        [Range(0.000,4.000)]
        public float UofUGPA { get; set; }

        // The number of hours the applicant has requested to work each week
        [Required]
        [Display(Name = "Hours Wanted", ShortName = "# Hours", Prompt = "10", Description = "Enter the number of hours you would like to work (5-20)")]
        [Range(5,20)]
        public int HoursRequested { get; set; }

        // Whether the applicant is available the week before the semester starts
        [Required]
        [Display(Name = "I am available the week before classes start", ShortName = "Available week before")]
        public bool IsAvailableWeekBefore { get; set; }

        // The number of semesters the applicant has completed at UofU
        [Required]
        [Display(Name = "Semesters completed at UofU", ShortName = "Semesters done", Prompt = "0", Description = "Enter the number of semesters you have completed at the University of Utah")]
        [Range(0, 200)]
        public int SemestersCompleted { get; set; }

        // A personal statement from the applicant detailing why they are applying to be a TA (optional)
        [Display(Name = "Please tell us why you are interested in being a TA", ShortName = "Personal statement", Prompt = "Type here...")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [DataType(DataType.MultilineText)]
        [StringLength(50000)]
        public string? Statement { get; set; }

        // The University that the applicant transferred from (if applicable)
        [Display(Name = "Previous University (if applicable)", ShortName = "Transfered from", Prompt = "Type here...")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(150)]
        public string? TransferSchool { get; set; }

        // A URL to the applicant's LinkedIn page (optional)
        [Display(Name = "LinkedIn URL", ShortName = "LinkedIn", Prompt = "Paste URL...")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(1000)]
        [Url]
        public string? LinkedInURL { get; set; }

        // The file name of the applicant's resume that they uploaded
        [Display(Name = "Resume file name", ShortName = "Resume", Prompt = "Resume.pdf", Description = "'Upload' your resume here")]
        [RegularExpression("^.*\\.(pdf)$", ErrorMessage = "This file must be a pdf!")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(1000)]
        public string? ResumeFileName { get; set; }
    }
}
