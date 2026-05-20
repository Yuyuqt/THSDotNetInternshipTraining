using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace THSDotNetTraining.AdoDotNetSample.ConsoleApp
{
    public class AdoDotNetService
    {

        string connectionString = "Data Source=.;Initial Catalog=Practice;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";


        //Read
        public void Read()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT [StudentId], [StudentNo], [StudentName], [FatherName], [DateOfBirth], [Gender], [Address], [MobileNo], [DeleteFlag]
                                 FROM [dbo].[Tbl_Student]
                                 WHERE [DeleteFlag] = 0";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string no = dr["StudentNo"].ToString();
                    string name = dr["StudentName"].ToString();
                    Console.WriteLine($"{i + 1} {no} - {name}");
                }

                connection.Close();
            }
        }

        // Create 

        public void Create()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO [dbo].[Tbl_Student] ([StudentNo], [StudentName], [FatherName], [DateOfBirth], [Gender], [Address], [MobileNo], [DeleteFlag])
                                 VALUES (@StudentNo, @StudentName, @FatherName, @DateOfBirth, @Gender, @Address, @MobileNo, @DeleteFlag)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StudentNo", Prompt("StudentNo"));
                cmd.Parameters.AddWithValue("@StudentName", Prompt("Student Name"));
                cmd.Parameters.AddWithValue("@FatherName", Prompt("Father Name"));
                cmd.Parameters.AddWithValue("@DateOfBirth", PromptDate("Date of Birth (yyyy-MM-dd)"));
                cmd.Parameters.AddWithValue("@Gender", Prompt("Gender (M/F)"));
                cmd.Parameters.AddWithValue("@Address", Prompt("Address"));
                cmd.Parameters.AddWithValue("@MobileNo", Prompt("Mobile No"));
                cmd.Parameters.AddWithValue("@DeleteFlag", 0);
                int result = cmd.ExecuteNonQuery();

                connection.Close();

                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                Console.WriteLine(message);
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

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StudentId", PromptInt("StudentId"));
                cmd.Parameters.AddWithValue("@StudentName", Prompt("New Student Name"));
                int result = cmd.ExecuteNonQuery();
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

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@StudentId", PromptInt("StudentId to delete (soft delete)"));
                int result = cmd.ExecuteNonQuery();
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