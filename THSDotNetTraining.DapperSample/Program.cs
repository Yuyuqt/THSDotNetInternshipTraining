using System;
using THSDotNetTraining.DapperSample.ConsoleApp;


namespace THSDotNetTraining.DapperSample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            
            DapperService dapperService = new DapperService();

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
                        
                        dapperService.Read();
                        break;

                    case 2:
                        Console.WriteLine("Adding New Data...");
          
                        dapperService.Create();
                        break;

                    case 3:
                        Console.WriteLine("Updating Data...");

                        dapperService.Update();
                        break;

                    case 4:
                        Console.WriteLine("Deleting Data...");

                        dapperService.Delete();
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