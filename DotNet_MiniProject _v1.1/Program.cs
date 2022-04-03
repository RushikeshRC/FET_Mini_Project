using System;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DotNet_MiniProject.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace DotNet_MiniProject
{
    class Program
    {
        static void Main(string[] args)
        {  start1 :
            switch (MainView.login())
            {
                case (int)User.Exit:

                    Environment.Exit(0);
                    break;

                case (int)User.Registration:
                    {
                        StudentManager.registerStudent();
                        // MainView.login();
                        goto start1;
                        //Console.ReadLine();
                        break;
                    }

                case (int)User.Student:

                    string studentUserName = StudentManager.getUserName();

                    Student student = StudentManager.getPassword(studentUserName);

                    do
                    {
                    START:
                        switch (StudentView.menu())
                        {
                            case (int)StudentQuery.Exit:

                                Environment.Exit(0);
                                break;



                            //case (int)StudentQuery.EnterDetails:

                            //    //var newCourses = StudentManager.createStudent(student);
                            //    //if (newCourses.Count != 0)
                            //    //{
                            //    //    var enrollInput = StudentView.enrollMenu(newCourses);
                            //    //    var newCourseID = newCourses[enrollInput].Id;
                            //    //    StudentManager.enrollStudent(student, newCourseID);
                            //    //}
                            //    //else StudentView.enrollFail();
                            //    //break;

                            //    StudentManager.createStudent(StudentView.createStudent());
                            //    break;

                            case (int)StudentQuery.Enroll:
                                {
                                    var newCourses = StudentManager.getNewCourses(student);
                                    if (newCourses.Count != 0)
                                    {
                                        var enrollInput = StudentView.enrollCourse(newCourses);
                                        var newCourseID = newCourses[enrollInput].CourseId;
                                        StudentManager.enrollStudent(student, newCourseID);
                                    }
                                    else StudentView.enrollFail();
                                    break;
                                }

                            case (int)StudentQuery.Update:
                                {
                                    // var students = StudentManager.getStudents(student);
                                     //var studentInput = StudentView.studentMenu(students);
                                    //StudentManager.updateStudent(students[studentInput]);
                                    StudentManager.updateStudent(student);


                                }
                                break;

                            case (int)StudentQuery.Delete:
                                {
                                    var students = StudentManager.getStudents(student);
                                    var studentInput = StudentView.studentMenu(students);
                                    StudentManager.deleteStudent(students[studentInput]);
                                }
                                break;

                            case (int)StudentQuery.Show:
                                {
                                    StudentView.showStudents(StudentManager.getStudents(student));
                                }
                                break;

                            case (int)StudentQuery.MainMenu:
                                {
                                    goto START;
                                }
                                break;
                                //case (int)StudentQuery.Schedule:

                                //    var currentCourses = StudentManager.getCurrentCourses(student);
                                //    StudentView.showSchedule(currentCourses);
                                //    break;

                                //case (int)StudentQuery.SubmissionDates:

                                //    var currentAssignments = StudentManager.getCurrentAssignments(student);
                                //    StudentView.showSubmissionDates(currentAssignments);
                                //    break;
                        }
                    } while (true);

                case (int)User.Registration1:
                    {
                        AdminManager.registerAdmin();
                        // MainView.login();
                        goto start1;
                        //Console.ReadLine();
                        break;
                    }

                case (int)User.Admin:
                    string adminName = AdminManager.getUserName();
                    AdminManager.getPassword(adminName);

                    do
                    {
                    START:
                        switch (AdminView.menu())
                        {
                            case (int)AdminQuery.Exit:

                                Environment.Exit(0);
                                break;



                            case (int)AdminQuery.Courses:
                                switch (AdminView.entityCourseMenu("COURSES"))
                                {
                                    case (int)Action.Create:
                                        CourseManager.createCourse(CourseView.createCourse());
                                        break;

                                    case (int)Action.Show:
                                        CourseView.showCourse(CourseManager.getCourses());
                                        break;

                                    case (int)Action.Update:
                                        {
                                            var courses = CourseManager.getCourses();
                                            var courseInput = CourseView.updateCourseMenu(courses);
                                            CourseManager.updateCourse(courses[courseInput]);
                                            break;
                                        }
                                    case (int)Action.Delete:
                                        {
                                            var courses = CourseManager.getCourses();
                                            var courseInput = CourseView.deleteCourseMenu(courses);
                                            CourseManager.deleteCourse(courses[courseInput]);
                                            break;
                                        }
                                    case (int)Action.Back:
                                        goto START;
                                }
                                break;

                            case (int)AdminQuery.Students:
                                switch (AdminView.entityStudentMenu("STUDENTS"))
                                {
                                    case (int)Action.Create:

                                        AdminView.createStudent();
                                        break;

                                    case (int)Action.Show:

                                        AdminView.showStudents(AdminManager.getStudents());
                                        break;

                                    case (int)Action.Update:
                                        {
                                            //var students = AdminManager.getStudents();
                                            //var studentInput = AdminView.studentMenu(students);
                                            Student student1 = new Student();
                                            StudentManager.updateStudent(student1);
                                        }

                                        break;

                                    case (int)Action.Delete:
                                        {
                                            var students = AdminManager.getStudents();
                                            var studentInput = AdminView.studentMenu(students);
                                            StudentManager.deleteStudent(students[studentInput]);
                                        }
                                        break;
                                    case (int)Action.Back:
                                        goto START;

                                }
                                break;

                            case (int)AdminQuery.MainMenu:
                                {
                                    goto START;
                                }
                                break;
                        }
                    } while (true);

                    break;
            }

        }

    }
    class MyDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = RUSHIKESHC-VD\\SQL2017; Database =StudentManagement; User ID = sa; Password = cybage@123456");

        }


        //public override int SaveChanges() {
        //    //get {
        //    //    try
        //    //    {
        //            var selectedEntityList = ChangeTracker.Entries()
        //                                    .Where(x => x.Entity is IUpdateable &&
        //                                    (x.State == EntityState.Added || x.State == EntityState.Modified));

        //            //Gt user Name from  session or other authentication   
        //            // var userName = ChangeTracker.Entries().c.Where(s => s.StudentID.Equals(student.StudentID)).ToList(); ;

        //            foreach (var entity in selectedEntityList)
        //            {

        //                ((IUpdateable)entity.Entity).CreatedDate = DateTime.Now;
        //                ((IUpdateable)entity.Entity).UpdatedDate = DateTime.Now;
        //                // ((IUpdateable)entity.Entity).ModifiedBy = userName;
        //            }

        //            //base.Add(selectedEntityList);
        //            //Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[students] ON");
        //            //SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT [dbo].[students] ON");
        //            //cmd.ExecuteNonQuery;
        //            // base.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[students] ON");


        //            //base.SaveChanges();

        //            //base.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[students] OFF");

        //            //transaction.Commit();
        //            //var transaction = base.Database.BeginTransaction();
        //            //transaction.Commit();
        //            //Console.WriteLine(e.Message);
        //            return base.SaveChanges();
        //        }
        //    }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IUpdateable && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IUpdateable)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IUpdateable)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }


        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Admin> admins { get; set; }

    }

    internal enum User
    { Exit, Registration, Student,Registration1, Admin }
    internal enum StudentQuery
    { Exit,Enroll, Update, Delete, Show, MainMenu }

    internal enum AdminQuery
    { Exit, Courses, Students, MainMenu }

    internal enum Action
    { Create, Show, Update, Delete, Back  }
}