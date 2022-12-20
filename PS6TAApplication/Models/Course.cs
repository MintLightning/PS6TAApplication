/**
 * Author:   Matt McFarlane
 * Date:      12/15/2022
 * Course:    CS 4540, University of Utah, School of Computing
 * Copyright: CS 4540 and McFarlane - This work may not be copied for use in Academic Coursework.
 *
 * I, Matt McFarlane, certify that I wrote this code from scratch and did not copy it in part or whole from
 * another source.  Any references used in the completion of the assignment are cited in my README file.
 *
 * File Contents
 *
 *    Object structure representing a Course
 */

using System.ComponentModel.DataAnnotations;

namespace PS6TAApplication.Models
{
    public class Course : ModificationTracking
    {
        // The different semesters a course can be offered
        public enum semester
        {
            Spring,
            Summer,
            Fall
        }
        public int ID { get; set; }

        //Semester course offered
        [Required]
        [Display(Name = "Semester Course Offered", ShortName = "Semester", Description = "The semester that the course is offered")]
        public semester SemesterOffered { get; set; }

        //Year course offered
        [Required]
        [Display(Name = "Year Course Offered", ShortName = "Year", Description = "The year that the course is offered")]
        public int YearOffered { get; set; }

        //Title of the course(e.g., Web Software)
        [Required]
        [Display(Name = "Course Title", ShortName = "Title", Description = "The title of the course")]
        public string Title { get; set; }

        //Department(e.g., CS, CE, Comp) that the course is in
        [Required]
        [Display(Name = "Department", ShortName = "Dpt.",Description = "Department that the course is in")]
        [StringLength(50)]
        public string Department { get; set; } = "";

        //Course Number(e.g., 2420)
        [Required]
        [Display(Name = "Course Number", ShortName = "Num.", Description = "Number used to identify the course")]
        public int Number { get; set; }

        //Section(e.g., 001)
        [Required]
        [Display(Name = "Section", ShortName = "Sec.", Description = "Subsection of the course")]
        [StringLength(10)]
        public string Section { get; set; } = "";

        //Description(i.e., the course catalog description)
        [Required]
        [Display(Name = "Description", ShortName = "Descr.", Description = "Course description")]
        [StringLength(10000)]
        public string Description { get; set; } = "";

        //Professor UNID
        [Required]
        [Display(Name = "Professor uID", ShortName = "Prof. uID", Description = "The uID of the professor teaching the course")]
        [RegularExpression("^u[0-9]{7}$", ErrorMessage = "Invalid uID. Must be in the format uXXXXXXX")]
        [StringLength(8)]
        public string ProfessorUNID { get; set; } = "";

        //Professor Name
        [Required]
        [Display(Name = "Professor Name", ShortName = "Prof.", Description = "The name of the professor teaching the course")]
        [StringLength(75)]
        public string ProfessorName { get; set; } = "";

        //Time and Days offered(e.g., M/W 3:30-5:00)
        [Required]
        [Display(Name = "Schedule", ShortName = "Sched.", Description = "Time and days offered")]
        [StringLength(200)]
        public string Schedule { get; set; } = "";

        //Location(e.g., WEB L 104)
        [Required]
        [Display(Name = "Location", ShortName = "Loc.", Description = "Course location")]
        [StringLength(100)]
        public string Location { get; set; } = "";

        //Credit Hours(e.g., 3)
        [Required]
        [Display(Name = "Credit Hours", ShortName = "Credits", Description = "Number of credit hours for the course")]
        public int CreditHours { get; set; }

        //Enrollment(i.e., how many students are enrolled, e.g., 150)
        [Required]
        [Display(Name = "Students Enrolled", ShortName = "Enrolled", Description = "Number of students enrolled in the course")]
        public int Enrollment { get; set; }

        //Note(a place for the site admin to make notes about the course, e.g., "Needs Extra TAs!")
        [Required]
        [Display(Name = "Admin Note", ShortName = "Note", Description = "A place for the site admin to make notes about the course")]
        [StringLength(10000)]
        public string AdminNote { get; set; } = "None";
    }
}
