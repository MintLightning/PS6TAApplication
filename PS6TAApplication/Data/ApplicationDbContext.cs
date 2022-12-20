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
 *    Handles database seeding to initialize database
 */

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using PS6TAApplication.Areas.Data;
using PS6TAApplication.Models;

namespace PS6TAApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAUser>()
                .HasOne(b => b.Application)
                .WithOne(i => i.TAUser)
                .HasForeignKey<Application>(b => b.TAUserId);
            base.OnModelCreating(modelBuilder);
        }

        //Database seeding, initializes 5 users and 3 roles
        public async Task InitializeUsers(ApplicationDbContext d, UserManager<TAUser> u, RoleManager<IdentityRole> r)
        {
            d.Database.EnsureCreated();

            if (r.Roles.Count() == 0)
            {
                await r.CreateAsync(new IdentityRole("Applicant"));
                await r.CreateAsync(new IdentityRole("Professor"));
                await r.CreateAsync(new IdentityRole("Admin"));
            }
            if (u.Users.Count() == 0)
            {
                TAUser user = new TAUser();
                user.UserName = "admin@utah.edu";
                user.Unid = "u1234567";
                user.Name = "Administrator";
                user.ReferredTo = "Admin";
                user.Email = "admin@utah.edu";
                user.EmailConfirmed = true;
                await u.CreateAsync(user, "123ABC!@#def");
                await u.AddToRoleAsync(user, "Admin");

                user = new TAUser();
                user.UserName = "professor@utah.edu";
                user.Unid = "u7654321";
                user.Name = "Professor";
                user.ReferredTo = "Prof";
                user.Email = "professor@utah.edu";
                user.EmailConfirmed = true;
                await u.CreateAsync(user, "123ABC!@#def");
                await u.AddToRoleAsync(user, "Professor");

                user = new TAUser();
                user.UserName = "u0000000@utah.edu";
                user.Unid = "u0000000";
                user.Name = "Student 1";
                user.ReferredTo = "Stud1";
                user.Email = "u0000000@utah.edu";
                user.EmailConfirmed = true;
                await u.CreateAsync(user, "123ABC!@#def");
                await u.AddToRoleAsync(user, "Applicant");

                user = new TAUser();
                user.UserName = "u0000001@utah.edu";
                user.Unid = "u0000001";
                user.Name = "Student 2";
                user.ReferredTo = "Stud2";
                user.Email = "u00000001utah.edu";
                user.EmailConfirmed = true;
                await u.CreateAsync(user, "123ABC!@#def");
                await u.AddToRoleAsync(user, "Applicant");

                user = new TAUser();
                user.UserName = "u0000002@utah.edu";
                user.Unid = "u0000002";
                user.Name = "Student 3";
                user.ReferredTo = "Stud3";
                user.Email = "u0000002@utah.edu";
                user.EmailConfirmed = true;
                await u.CreateAsync(user, "123ABC!@#def");
                await u.AddToRoleAsync(user, "Applicant");
            }
        }

        public async Task InitializeApplications(ApplicationDbContext d, UserManager<TAUser> u, RoleManager<IdentityRole> r)
        {
            if(d.Application.Count() == 0)
            {
                Application u0 = new Application();
                TAUser u0User = u.FindByNameAsync("u0000000@utah.edu").Result;
                u0.TAUser = u0User;
                u0.TAUserId = u0User.Id;
                u0.CurrentDegree = Models.Application.degree.BSMS;
                u0.Department = "CS";
                u0.UofUGPA = 3.739f;
                u0.HoursRequested = 12;
                u0.IsAvailableWeekBefore = true;
                u0.SemestersCompleted = 6;
                u0.Statement = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean vulputate imperdiet justo, at luctus felis dapibus ut. Nunc egestas ipsum non augue tempus, ac efficitur massa porttitor. Nulla tortor neque, bibendum quis sagittis vel, luctus id tortor. In ante nisl, convallis sed tempor sed, vehicula non magna.";
                u0.TransferSchool = "SLCC";
                u0.LinkedInURL = "https://www.linkedin.com/";
                u0.ResumeFileName = "u00.pdf";

                d.Add(u0);
                await d.SaveChangesAsync();

                u0User.Application = u0;
                u0User.ApplicationId = u0.ID;
                await u.UpdateAsync(u0User);


                Application u1 = new Application();
                TAUser u1User = u.FindByNameAsync("u0000001@utah.edu").Result;
                u1.TAUser = u1User;
                u1.TAUserId = u1User.Id;
                u1.CurrentDegree = Models.Application.degree.BS;
                u1.Department = "FILM";
                u1.UofUGPA = 3.152f;
                u1.HoursRequested = 5;
                u1.IsAvailableWeekBefore = false;
                u1.SemestersCompleted = 2;

                d.Add(u1);
                await d.SaveChangesAsync();

                u1User.Application = u1;
                u1User.ApplicationId = u1.ID;
                await u.UpdateAsync(u1User);
            }
            
        }

        public async Task InitializeCourses(ApplicationDbContext d)
        {
            if (d.Course.Count() == 0)
            {
                Course u0 = new Course();
                u0.SemesterOffered = Models.Course.semester.Spring;
                u0.YearOffered = 2023;
                u0.Title = "Introduction to Computer Programming";
                u0.Department = "CS";
                u0.Number = 1400;
                u0.Section = "001";
                u0.Description = "This course is an introduction to the engineering and mathematical skills required to effectively program computers and is designed for students with no prior programming experience.";
                u0.ProfessorUNID = "u0061533";
                u0.ProfessorName = "JOHNSON, DAVID";
                u0.Schedule = "MoWe/01:25PM-02:45PM";
                u0.Location = "S BEH AUD";
                u0.CreditHours = 4;
                u0.Enrollment = 150;
                u0.AdminNote = "Needs Extra TAs!";
                d.Add(u0);

                Course u1 = new Course();
                u1.SemesterOffered = Models.Course.semester.Spring;
                u1.YearOffered = 2023;
                u1.Title = "Object-Oriented Prog";
                u1.Department = "CS";
                u1.Number = 1410;
                u1.Section = "001";
                u1.Description = "This course builds on the programming skills learned in CS 1400, while introducing the paradigm of object-oriented programming.";
                u1.ProfessorUNID = "u9999999";
                u1.ProfessorName = "MARTIN, TRAVIS B";
                u1.Schedule = "MoWeFr/09:40AM-10:30AM";
                u1.Location = "S BEH AUD";
                u1.CreditHours = 4;
                u1.Enrollment = 237;
                u1.AdminNote = "None";
                d.Add(u1);

                Course u2 = new Course();
                u2.SemesterOffered = Models.Course.semester.Spring;
                u2.YearOffered = 2023;
                u2.Title = "Accelerated Introduction to Object-Oriented Programming";
                u2.Department = "CS";
                u2.Number = 1420;
                u2.Section = "001";
                u2.Description = "This course is an introduction to the engineering and mathematical skills required to effectively program computers, covering in a single semester the material of both CS 1400 and CS 1410, and is designed for students with prior programming experience.";
                u2.ProfessorUNID = "u0063547";
                u2.ProfessorName = "JENSEN, PETER";
                u2.Schedule = "MoWe/03:00PM-04:20PM";
                u2.Location = "GC 1900";
                u2.CreditHours = 4;
                u2.Enrollment = 156;
                u2.AdminNote = "None";
                d.Add(u2);

                Course u3 = new Course();
                u3.SemesterOffered = Models.Course.semester.Spring;
                u3.YearOffered = 2023;
                u3.Title = "Discrete Structures";
                u3.Department = "CS";
                u3.Number = 2100;
                u3.Section = "001";
                u3.Description = "Introduction to propositional logic, predicate logic, formal logical arguments, finite sets, functions, relations, inductive proofs, recurrence relations, graphs, probability, and their applications to Computer Science.";
                u3.ProfessorUNID = "u0877336";
                u3.ProfessorName = "Elhabian, Shireen";
                u3.Schedule = "MoWe/08:05AM-09:25AM";
                u3.Location = "WEB L104";
                u3.CreditHours = 3;
                u3.Enrollment = 259;
                u3.AdminNote = "None";
                d.Add(u3);

                Course u4 = new Course();
                u4.SemesterOffered = Models.Course.semester.Spring;
                u4.YearOffered = 2023;
                u4.Title = "Computer Systems";
                u4.Department = "CS";
                u4.Number = 4400;
                u4.Section = "001";
                u4.Description = "Introduction to computer systems from a programmer's point of view. Machine level representations of programs, optimizing program performance, memory hierarchy, linking, exceptional control flow, measuring program performance, virtual memory, concurrent programming with threads, network programming.";
                u4.ProfessorUNID = "u0297211";
                u4.ProfessorName = "KOPTA, DANIEL";
                u4.Schedule = "MoWe/11:50AM-01:10PM";
                u4.Location = "HEB 2004";
                u4.CreditHours = 3;
                u4.Enrollment = 198;
                u4.AdminNote = "This is an admin note for CS 4400";
                d.Add(u4);

                await d.SaveChangesAsync();
            }

        }
        //Database seeding, initializes 5 users and 3 roles and 5 courses

        public DbSet<PS6TAApplication.Models.Application> Application { get; set; }

        /// <summary>
        /// Every time Save Changes is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()  // JIM: Override save changes to add timestamps
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        
        /// <summary>
        /// Every time Save Changes (Async) is called, add timestamps
        /// </summary>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            AddTimestamps();   // JIM: Override save changes async to add timestamps
            return await base.SaveChangesAsync(cancellationToken);
        }
        
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is ModificationTracking
                        && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = "";

            if (_httpContextAccessor.HttpContext == null) // happens during startup/initialization code
            {
                currentUsername = "DBSeeder";
            }
            else
            {
                currentUsername = _httpContextAccessor.HttpContext.User.Identity?.Name;
            }

            currentUsername ??= "Sadness"; // JIM: compound assignment magic... test for null, and if so, assign value

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((ModificationTracking)entity.Entity).CreationDate = DateTime.UtcNow;
                    ((ModificationTracking)entity.Entity).CreatedBy = currentUsername;
                }
                ((ModificationTracking)entity.Entity).ModificationDate = DateTime.UtcNow;
                ((ModificationTracking)entity.Entity).ModifiedBy = currentUsername;
            }
        }
        
        /// <summary>
        /// JIM: this code adds time/user to DB entry
        /// 
        /// Check the DB tracker to see what has been modified, and add timestamps/names as appropriate.
        /// 
        /// </summary>
        public DbSet<PS6TAApplication.Models.Course> Course { get; set; }
    }
}