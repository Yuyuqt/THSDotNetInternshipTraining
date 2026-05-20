using System;
using THSDotNetTraining.EFCoreSample.ConsoleApp;

namespace SLHDotNetTrainingInPersonBatch0.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            
            EfCoreService efcoreservice = new EfCoreService();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== STUDENT MANAGEMENT SYSTEM ===");
                Console.WriteLine("1. Read");
                Console.WriteLine("2. Create");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Exit");
                Console.Write("Choose option: ");


                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid option. Please enter a valid number.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("\nReading data...");
                        efcoreservice.Read();
                        break;

                    case 2:
                        Console.WriteLine("\n--- Adding New Student Details ---");

                        Console.Write("Student No: ");
                        string sNo = Console.ReadLine();

                        Console.Write("Student Name: ");
                        string sName = Console.ReadLine();

                        Console.Write("Father Name: ");
                        string fName = Console.ReadLine();

                        Console.Write("Date of Birth (YYYY-MM-DD): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                        {
                            Console.WriteLine("Invalid date format. Falling back to today's date.");
                            dob = DateTime.Today;
                        }

                        Console.Write("Gender (M/F): ");
                        string gender = Console.ReadLine();

                        Console.Write("Address: ");
                        string address = Console.ReadLine();

                        Console.Write("Mobile No: ");
                        string mobile = Console.ReadLine();

                        // Calls the active service class dynamically
                        efcoreservice.Create(sNo, sName, fName, dob, gender, address, mobile);
                        break;

                    case 3:
                        Console.WriteLine("\nUpdating Data...");
                        Console.Write("Enter Student ID to update: ");

                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.Write("Enter New Student Name: ");
                            string updateName = Console.ReadLine();

                            Console.Write("Enter New Address: ");
                            string updateAddress = Console.ReadLine();

                            efcoreservice.Update(updateId, updateName, updateAddress);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format. Please enter a number.");
                        }
                        break;

                    case 4:
                        Console.WriteLine("\nDeleting Data...");
                        Console.Write("Enter Student ID to delete (Soft Delete): ");

                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            efcoreservice.Delete(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID format. Please enter a number.");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please pick options between 1 and 5.");
                        break;
                }
            }
        }
    }
}