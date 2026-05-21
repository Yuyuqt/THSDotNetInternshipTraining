using System;
using THSDotNetTraining.DapperSample;
using THSDotNetTraining.DapperSample.ConsoleApp;

namespace THSDotNetTraining.DapperSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            dapperService dapperService = new dapperService();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Student Management System (Dapper) ===");

                Console.WriteLine("1. Read ");
                Console.WriteLine("2. Create ");
                Console.WriteLine("3. Edit ");
                Console.WriteLine("4. Update ");
                Console.WriteLine("5. Delete ");
                Console.WriteLine("6. Exit");

                Console.Write("Choose option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Please enter a valid numeric option.");
                    continue;
                }

                switch (option)
                {
                    // Read 
                    case 1:
                        Console.WriteLine("\n--- Reading Student Data ---");
                        dapperService.Read();
                        break;

                    // Create
                    case 2:
                        Console.WriteLine("\n--- Adding New Student ---");
                        Student newStudent = new Student();

                        Console.Write("Enter Student No: ");
                        newStudent.StudentNo = Console.ReadLine();

                        Console.Write("Enter Student Name: ");
                        newStudent.StudentName = Console.ReadLine();

                        Console.Write("Enter Father Name: ");
                        newStudent.FatherName = Console.ReadLine();

                        Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                        {
                            newStudent.DateOfBirth = dob;
                        }
                        else
                        {
                            newStudent.DateOfBirth = DateTime.Now;
                            Console.WriteLine("Invalid date format. Saved as today's date.");
                        }

                        Console.Write("Enter Gender (Male/Female): ");
                        newStudent.Gender = Console.ReadLine();

                        Console.Write("Enter Address: ");
                        newStudent.Address = Console.ReadLine();

                        Console.Write("Enter Mobile No: ");
                        newStudent.MobileNo = Console.ReadLine();

                        dapperService.Create(newStudent);
                        break;

                    // Edit
                    case 3:
                        Console.WriteLine("\n--- Editing Student Data ---");
                        Console.Write("Enter Student ID to view details: ");

                        if (int.TryParse(Console.ReadLine(), out int editId))
                        {
                            Student student = dapperService.Edit(editId);
                            if (student != null)
                            {
                                Console.WriteLine($"\n--- Student Details for ID: {student.StudentId} ---");
                                Console.WriteLine($"No:         {student.StudentNo}");
                                Console.WriteLine($"Name:       {student.StudentName}");
                                Console.WriteLine($"Father:     {student.FatherName}");
                                Console.WriteLine($"DOB:        {student.DateOfBirth:dd MMM yyyy}");
                                Console.WriteLine($"Gender:     {student.Gender}");
                                Console.WriteLine($"Address:    {student.Address}");
                                Console.WriteLine($"Mobile:     {student.MobileNo}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID format.");
                        }
                        break;

                    //  Update
                    case 4:
                        Console.WriteLine("\n--- Updating Student Data ---");
                        Console.Write("Enter Student ID to update: ");
                        if (!int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.WriteLine("Invalid Student ID format.");
                            break;
                        }

                        Student targetStudent = dapperService.Edit(updateId);
                        if (targetStudent == null) break;

                        Console.WriteLine($"Updating details for {targetStudent.StudentName}. Press Enter to keep current values.");

                        Console.Write($"Enter New Student No [{targetStudent.StudentNo}]: ");
                        string inputNo = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputNo)) targetStudent.StudentNo = inputNo;

                        Console.Write($"Enter New Student Name [{targetStudent.StudentName}]: ");
                        string inputName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputName)) targetStudent.StudentName = inputName;

                        Console.Write($"Enter New Father Name [{targetStudent.FatherName}]: ");
                        string inputFather = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputFather)) targetStudent.FatherName = inputFather;

                        Console.Write($"Enter New DOB (yyyy-MM-dd) [{targetStudent.DateOfBirth:yyyy-MM-dd}]: ");
                        string inputDob = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputDob) && DateTime.TryParse(inputDob, out DateTime newDob))
                        {
                            targetStudent.DateOfBirth = newDob;
                        }

                        Console.Write($"Enter New Gender [{targetStudent.Gender}]: ");
                        string inputGender = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputGender)) targetStudent.Gender = inputGender;

                        Console.Write($"Enter New Address [{targetStudent.Address}]: ");
                        string inputAddress = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputAddress)) targetStudent.Address = inputAddress;

                        Console.Write($"Enter New Mobile No [{targetStudent.MobileNo}]: ");
                        string inputMobile = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(inputMobile)) targetStudent.MobileNo = inputMobile;

                        dapperService.Update(targetStudent);
                        break;

                    // Delete
                    case 5:
                        Console.WriteLine("\n--- Deleting Student Data ---");
                        Console.Write("Enter Student ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            dapperService.Delete(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID format.");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 6.");
                        break;
                }
            }
        }
    }
}