using System;
using Microsoft.Data.SqlClient;
using Dapper;  // Dapper namespace

namespace WintThinMayPractice.ConsoleApp
{
    public class DapperService
    {

        string connectionString = "Data Source=.;Initial Catalog=Practice;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

        // Read 
        public void Read()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT [StudentId], [StudentNo], [StudentName], [FatherName], [DateOfBirth], [Gender], [Address], [MobileNo], [DeleteFlag]
                                 FROM [dbo].[Tbl_Student]
                                 WHERE [DeleteFlag] = 0";


                var students = connection.Query(query);


                foreach (var student in students)
                {
                    Console.WriteLine($"{student.StudentNo} - {student.StudentName}");
                }

                connection.Close();
            }
        }

        //Create 
        public void Create()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO [dbo].[Tbl_Student] ([StudentNo], [StudentName], [FatherName], [DateOfBirth], [Gender], [Address], [MobileNo], [DeleteFlag])
                                 VALUES (@StudentNo, @StudentName, @FatherName, @DateOfBirth, @Gender, @Address, @MobileNo, @DeleteFlag)";

                var parameters = new
                {
                    StudentNo = Prompt("StudentNo"),
                    StudentName = Prompt("Student Name"),
                    FatherName = Prompt("Father Name"),
                    DateOfBirth = PromptDate("Date of Birth (yyyy-MM-dd)"),
                    Gender = Prompt("Gender (M/F)"),
                    Address = Prompt("Address"),
                    MobileNo = Prompt("Mobile No"),
                    DeleteFlag = 0
                };


                var result = connection.Execute(query, parameters);

                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                Console.WriteLine(message);

                connection.Close();
            }
        }

        // Update
        public void Update()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE [dbo].[Tbl_Student]
                                 SET [StudentName] = @StudentName
                                 WHERE [StudentId] = @StudentId";

                var parameters = new
                {
                    StudentId = PromptInt("StudentId"),
                    StudentName = Prompt("New Student Name"),
                };

                // Using Dapper to execute the update query
                var result = connection.Execute(query, parameters);
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                connection.Close();

                Console.WriteLine(message);
            }
        }

        // Delete
        public void Delete()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE [dbo].[Tbl_Student]
                                 SET DeleteFlag = 1
                                 WHERE StudentId = @StudentId";

                var parameters = new { StudentId = PromptInt("StudentId to delete (soft delete)") };

                // Using Dapper to execute the delete query
                var result = connection.Execute(query, parameters);
                string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

                connection.Close();

                Console.WriteLine(message);
            }
        }

        private static string Prompt(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine() ?? string.Empty;
        }

        private static int PromptInt(string label)
        {
            Console.Write($"{label}: ");
            return int.Parse(Console.ReadLine());
        }

        private static bool PromptBool(string label)
        {
            Console.Write($"{label}: ");
            string value = (Console.ReadLine() ?? "0").Trim();
            return value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase)
                || value.Equals("yes", StringComparison.OrdinalIgnoreCase)
                || value.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        private static DateTime PromptDate(string label)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime value))
                {
                    return value;
                }
                Console.WriteLine("Invalid date format. Example: 2000-01-01");
            }
        }
    }
}