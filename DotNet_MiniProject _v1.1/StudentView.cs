﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet_MiniProject.Models;

namespace DotNet_MiniProject
{
    internal class StudentView
    {
        public static int menu()
        {
            int userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("                                        STUDENT MENU");
                Console.WriteLine("--------------------------------------------------------------------------------------");
                Console.WriteLine("Press 0 to Exit Application");
                //Console.WriteLine("Press 1 to Enter Student Deatils");
                Console.WriteLine("Press 1 to Enroll to a Course");
                Console.WriteLine("Press 2 to Update Student Deatils");
                Console.WriteLine("Press 3 to Delete Student Deatils");
                Console.WriteLine("Press 4 to Show Student Deatils");
                Console.WriteLine("Press 5 for Back to Main Menu");
            } while (!int.TryParse(Console.ReadLine(), out userInput));
            return userInput;
        }


        public static int enrollCourse(List<Course> courses)
        {
            Console.WriteLine("hggnbein");
            int userInput;
            do
            {
                for (int i = 0; i < courses.Count; i++)
                {
                    Console.WriteLine($"Press {i}. to enroll to the course: {courses[i].ToString()}");
                }
            } while (!int.TryParse(Console.ReadLine(), out userInput)); 
            return userInput;
        }

        public static void enrollFail()
        {
            Console.WriteLine();
            Console.WriteLine("There are no available courses for you");
            Console.WriteLine("Press any key to continue..");
            Console.ReadLine();
        }

        //internal static Student createStudent()
        //{
        //    Console.WriteLine("Enter Student name:");
        //    var studentname = Console.ReadLine();
        //    DateTime DateOfBirth;
        //    do
        //    {
        //        Console.WriteLine("Enter birthdate:");
        //    } while (!DateTime.TryParse(Console.ReadLine(), out DateOfBirth));

        //   // Console.WriteLine("hgjhb");
        //    Console.WriteLine("Enter Email:");
        //    string email ="";
        //    bool emailCheck = Validation.isValidEmail(Console.ReadLine());
        //    if (emailCheck == true)
        //    {
        //         email = Console.ReadLine();
        //    }
        //    else
        //    {
        //        Console.WriteLine("please enter email in correct format");
        //    }


        //    Console.WriteLine("Enter Location : ");
        //    var location = Console.ReadLine();
        //    Console.WriteLine("Enter Gender (M/F): ");
        //    var gender = Console.ReadLine();
        //    Console.WriteLine("Enter Contact No : ");
        //    var contactno = Console.ReadLine();
        //    Student studnt = new Student { StudentName = studentname, DateOfBirth = DateOfBirth, Email = email, Location = location, Gender = gender, ContactNo = contactno };

        //    return studnt;
        //}

        //Show Student
        internal static void showStudents(List<Student> students)
        {
            using (var context = new MyDbContext())
            {
                var header = String.Format("-----------------------------------------------------------------------------------------------------------------\n" +
                    "\t\t\t\t\tUsers List\n" +
                    "-----------------------------------------------------------------------------------------------------------------\n" +
                    "{0,4}{1,15}{2,20}{3,10}{4,20}{5,20}{6,25}{7,30}{8,35}\n-----------------------------------------------------------------------------------------------------------------",
                    "StudentId", "StudentName", "D.o.B", "Email", "Gender", "Location", "ContactNo","CreateDate","UpdateDate");
                Console.WriteLine(header);
                foreach (var s in students)
                {
                    //Console.WriteLine(s.ToString());
                    var output = String.Format("{0,4}{1,15}{2,30}{3,10}{4,20}{5,20}{6,25}{7,30}{8,35}", s.StudentID, s.StudentName, s.DateOfBirth, s.Email, s.Gender, s.Location, s.ContactNo,s.CreatedDate,s.UpdatedDate);
                    Console.WriteLine(output);
                }
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine("Press any key to continue..");
            Console.ReadLine();
        }

        internal static int studentMenu(List<Student> students)
        {
            int userInput;
            do
            {
                for (int i = 0; i < students.Count; i++)
                {
                    Console.WriteLine($"Press {i} to delete student: {students[i].ToString()}");
                }
            } while (!int.TryParse(Console.ReadLine(), out userInput));
            return userInput;
        }
    }
}
