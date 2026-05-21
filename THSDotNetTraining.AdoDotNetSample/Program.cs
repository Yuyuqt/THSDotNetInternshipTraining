using System;

using THSDotNetTraining.AdoDotNetSample.ConsoleApp;

namespace THSDotNetTraining.AdoDotNetSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            AdoDotNetService adoService = new AdoDotNetService();
            
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Read");
                Console.WriteLine("2. Create");
                Console.WriteLine("3. Update");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Exit");
                Console.Write("Choose option: ");

                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Reading data...");
                        adoService.Read();
                        
                        break;

                    case 2:
                        Console.WriteLine("Adding New Data...");
                        adoService.Create();
                        
                        break;

                    case 3:
                        Console.WriteLine("Updating Data...");
                        adoService.Update();
                        
                        break;

                    case 4:
                        Console.WriteLine("Deleting Data...");
                        adoService.Delete();
                        
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 5.");
                        break;
                }
            }
        }
    }
}