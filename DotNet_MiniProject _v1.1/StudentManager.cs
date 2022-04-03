using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using DotNet_MiniProject.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNet_MiniProject
{
    class StudentManager 
    {
        static string password, studentName, email, location, gender, contactNo;
        private static DateTime dob;
        static Student student = new Student();
        //Student Registration
        public static void registerStudent()
        {
            //email check
            DateTime dob;
            bool emailCheck;
            do
            {
                Console.WriteLine("Enter Email:");
                string emailCatch;
                emailCatch = Console.ReadLine();
                emailCheck = Validation.isValidEmail(emailCatch);
                if (emailCheck == true)
                {
                    email = emailCatch;
                }
                else
                {
                    Console.WriteLine("please enter email in correct format");

                }

            } while (emailCheck != true);

            //pass check
            bool passCheck;
            do
            {
                Console.WriteLine("Enter Password:");
                string pass, errormsg;
                pass = Console.ReadLine();
                passCheck = Validation.isValidPassword(pass, out errormsg);
                if (passCheck == true)
                {
                    password = pass;
                }
                else
                {
                    Console.WriteLine(errormsg);
                }

            } while (passCheck != true);

            Console.WriteLine("Please Enter StudentName :");
            studentName = Console.ReadLine();

            Console.WriteLine("Please Enter dob :");
            DateTime.TryParse(Console.ReadLine(), out dob);

         
            //location check
            bool locationCheck;
            do
            {
                Console.WriteLine("Enter Location:");
                string locationCatch;
                locationCatch = Console.ReadLine();

                locationCheck = Validation.isValidName(locationCatch);


                if (locationCheck == true)
                {
                    location = locationCatch;
                }
                else
                {
                    Console.WriteLine("please enter location in correct format");

                }

            } while (locationCheck != true);
            

            //Gender Check
            bool genderCheck;
            do
            {
                Console.WriteLine("Please select Gender (M/F):");
                string genderCatch;
                genderCatch = Console.ReadLine();


                genderCheck = Validation.isValidGender(genderCatch);


                if (genderCheck == true)
                {
                    gender = genderCatch;
                }
                else
                {
                    Console.WriteLine("please enter gender in correct format");

                }

            } while (genderCheck != true);

            
            //Contact check
            bool contactCheck;
            do
            {
                Console.WriteLine("Please enter Contact No. :");
                string contactCatch;
                contactCatch = Console.ReadLine();

                contactCheck = Validation.isValidContact(contactCatch);


                if (contactCheck == true)
                {
                    contactNo = contactCatch;
                }
                else
                {
                    Console.WriteLine("please enter contact no. in correct format(10 digit without country code)");

                }

            } while (contactCheck != true);


            using (var add = new MyDbContext())
            {
                
                student.Email = email;
                student.Password = password;
                student.StudentName = studentName;
                student.DateOfBirth = dob;
                student.Location = location;
                student.Gender = gender;
                student.ContactNo = contactNo;
                

                add.Add(student);

                add.SaveChanges();

                Console.WriteLine("Registration Successful");
                Console.ReadLine();
            }

            
    }

    
    public static string getUserName()
        {
            //Student student;
            string email;
            do
            {
                Console.WriteLine("Enter Email : ");
                email = Console.ReadLine();
                using(var context = new MyDbContext())
                {
                    student = context.students.SingleOrDefault(s => s.Email.Equals(email));
                }
            } while (student == null);
            return email;
        }

        public static Student getPassword(string email)
        {
            string password;
            //Student student;
            do
            {
                Console.WriteLine("Please insert password");
                password = Console.ReadLine();

                using (var context = new MyDbContext())
                {
                    student = context.students.SingleOrDefault(s => s.Password.Equals(password) && s.Email.Equals(email));
                }
            } while (student == null);
            return student;
        }

         // Student Courses 
        public static List<Course> getNewCourses(Student student)//get courses where student isnt enrolled
        {
            List<Course> newCourses;
            using (var context = new MyDbContext())
            {
                newCourses = context.courses.Where(c => c.Students.All(s => s.StudentID != student.StudentID)).ToList();
            }
            return newCourses;
        }

        public static List<Course> getCurrentCourses(Student student) //get courses where student is enrolled
        {
            List<Course> currentCourses;
            using (var context = new MyDbContext())
            {
                currentCourses = context.courses.Where(c => c.Students.Any(s => s.StudentID.Equals(student.StudentID))).ToList();
            }
            return currentCourses;
        }

        public static void enrollStudent(Student student, int newCourseID)
        {
            using (var context = new MyDbContext())
            {
                context.Entry(student).State = EntityState.Unchanged;
                context.courses.SingleOrDefault(c => c.CourseId.Equals(newCourseID)).Students.Add(student);
                context.SaveChanges();
            }
        }


        public static List<Student> getStudents(Student student) //get courses where student is enrolled
        {
            List<Student> currentStudent;
            using (var context = new MyDbContext())
            {
                //currentStudent = (List<Student>)context.students.Where(s => s.StudentID.Equals(student.StudentID));
                currentStudent = context.students.Where(s => s.StudentID.Equals(student.StudentID)).ToList();
            }
            return currentStudent;
        }

        // Student Update
        internal static void updateStudent(Student std)
        {

            Console.WriteLine("Hello " + std.StudentName);
            bool flagUp = true;
            do
            {
                Console.WriteLine("1)Update StudentName\n2)Update DateOfBirth\n3)Update Location No\n4)Update Gender\n5)Update ContactNo\n6)Exit");
               
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("Please Enter StudentName :");
                        std.StudentName = Console.ReadLine();

                        break;
                    case 2:
                        DateTime DateOfBirth;
                        do
                        {
                            Console.WriteLine("Update birthdate:");
                        } while (!DateTime.TryParse(Console.ReadLine(), out DateOfBirth));
                        break;
                    case 3:
                        bool locationCheck;
                        do
                        {
                            Console.WriteLine("Enter Location:");
                            string locationCatch;
                            locationCatch = Console.ReadLine();

                            locationCheck = Validation.isValidName(locationCatch);


                            if (locationCheck == true)
                            {
                                location = locationCatch;
                            }
                            else
                            {
                                Console.WriteLine("please enter location in correct format");

                            }

                        } while (locationCheck != true);
                        break;
                    case 4:
                        bool genderCheck;
                        do
                        {
                            Console.WriteLine("Please select Gender (M/F):");
                            string genderCatch;
                            genderCatch = Console.ReadLine();


                            genderCheck = Validation.isValidGender(genderCatch);


                            if (genderCheck == true)
                            {
                                gender = genderCatch;
                            }
                            else
                            {
                                Console.WriteLine("please enter gender in correct format");

                            }

                        } while (genderCheck != true);
                        break;
                    case 5:
                        bool contactCheck;
                        do
                        {
                            Console.WriteLine("Please enter Contact No. :");
                            string num = Console.ReadLine();
                            contactCheck = Validation.isValidContact(num);
                            if (contactCheck == true)
                            {
                                student.ContactNo = num;
                            }
                            else
                            {
                                Console.WriteLine("please enter contact no. in correct format(10 digit without country code)");

                            }

                        } while (contactCheck != true);
                        break;


                    case 6:
                        flagUp = false;
                        break;
                    default:
                        Console.WriteLine("Please Enter valid credentials ");
                        break;
                }
            } while (flagUp);



            using (var context = new MyDbContext())
            {
              
                context.students.Update(student);
                context.SaveChanges();
                Console.WriteLine("Updated Successfully");

                student.StudentName = std.StudentName;
                student.DateOfBirth = std.DateOfBirth;
                student.Location = std.Location;
                student.Gender = std.Gender;
                student.ContactNo = std.ContactNo;
                context.Add(student);
                context.SaveChanges();
                Console.WriteLine("Data Updateded Successfully !");
            }

        }

        //Delete Student
        internal static void deleteStudent(Student student)
        {
            using (var context = new MyDbContext())
            {
                context.Entry(student).State = EntityState.Unchanged;
                context.students.Remove(context.students.SingleOrDefault(sa => sa.StudentID == student.StudentID));
                context.students.Remove(student);
                context.SaveChanges();
                Console.WriteLine("Deleted Student Successfully !");
            }
        }
    }
}
