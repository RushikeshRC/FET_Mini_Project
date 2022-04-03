using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet_MiniProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNet_MiniProject
{
    class AdminManager
    {
        static string Email, Password;
        static string password, username, studentName, email, location, gender, contactNo, createdDate, updatedDate;
        private static DateTime dob;
        static Student student = new Student();

        //Admin Register
        public static void registerAdmin()
        {

            //email check
            bool emailCheck;
            do
            {
                Console.WriteLine("Enter Email:");

                string emailCatch;
                emailCatch = Console.ReadLine();


                emailCheck = Validation.isValidEmail(emailCatch);


                if (emailCheck == true)
                {
                    Email = emailCatch;
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

                // error = "incorrect";
                if (passCheck == true)
                {
                    Password = pass;
                }
                else
                {
                    Console.WriteLine(errormsg);
                }

            } while (passCheck != true);



            using (var add = new MyDbContext())
            {
                Admin admin = new Admin();

                
                admin.Email = Email;
                admin.Password = Password;
                add.Add(admin);

                add.SaveChanges();

                Console.WriteLine("Registration Successful");
                Console.ReadLine();
            }

        }

           //Get Email
            public static string getUserName()
        {
            Admin admin;
            string email;
            do
            {
                Console.WriteLine("Enter Email : ");
                email = Console.ReadLine();
                using (var context = new MyDbContext())
                {
                    admin = context.admins.SingleOrDefault(s => s.Email.Equals(email));
                }
            } while (admin == null);
            return email;
        }

        public static Admin getPassword(string email)
        {
            string password;
            Admin admin;
            do
            {
                Console.WriteLine("Please insert password");
                password = Console.ReadLine();

                using (var context = new MyDbContext())
                {
                    admin = context.admins.SingleOrDefault(s =>s.Password.Equals(password) && s.Email.Equals(email));
                }
            } while (admin == null);
            return admin;
        }

        public static void createStudent(Student studnt)
        {
            using (var context = new MyDbContext())
            {
                context.students.Add(studnt);
                context.SaveChanges();
                Console.WriteLine("Added Student Successfully !");
            }
        }

        internal static List<Student> getStudents()
        {
            List<Student> students;
            using (var context = new MyDbContext())
            {
                students = context.students.ToList();
            }
            return students;
        }

        //Admin Update Student
        internal static void updateStudent(Student std)
        {
            //Console.WriteLine("Update Student name:");
            //var studentname = Console.ReadLine();
            //DateTime DateOfBirth;
            //do
            //{
            //    Console.WriteLine("Update birthdate:");
            //} while (!DateTime.TryParse(Console.ReadLine(), out DateOfBirth));
            //Console.WriteLine("Update Email:");
            //var email = Console.ReadLine();
            //Console.WriteLine("Update Location : ");
            //var location = Console.ReadLine();
            //Console.WriteLine("Update Gender : ");
            //var gender = Console.ReadLine();
            //Console.WriteLine("Update Contact No : ");
            //var contactno = Console.ReadLine();

            Console.WriteLine("Hello " + student.StudentName);
            bool flagUp = true;
            do
            {
                Console.WriteLine("1)Update StudentName\n2)Update DateOfBirth\n3)Update Location No\n4)Update Gender\n5)Update ContactNo\n6)Exit");

                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("Please Enter StudentName :");
                        studentName = Console.ReadLine();

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

        internal static void deleteStudent(Student student)
        {
            using (var context = new MyDbContext())
            {
                context.Entry(student).State = EntityState.Unchanged;
                context.students.Remove(context.students.SingleOrDefault(sa => sa.StudentID == student.StudentID));
                context.students.Remove(student);
                context.SaveChanges();
                Console.WriteLine("Deleted Student Successfully !");
                Console.ReadLine();
            }
        }
    }
}
