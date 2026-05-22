using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper; // <-- Don't forget this!
using THSDotNetTraining.DapperSample;

namespace THSDotNetTraining.DapperSample.ConsoleApp
{
    public class DapperService
    {
        private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "Practice",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        public void Read()
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);

            string query = @"SELECT [StudentId]
                                  ,[StudentNo]
                                  ,[StudentName]
                                  ,[FatherName]
                                  ,[DateOfBirth]
                                  ,[Gender]
                                  ,[Address]
                                  ,[MobileNo]
                                  ,[DeleteFlag]
                                  ,[CreatedDateTime]
                                  ,[CreatedBy]
                                  ,[ModifiedDateTime]
                                  ,[ModifiedBy]
                              FROM [dbo].[Tbl_Student] WHERE [DeleteFlag] = 0";

            // Dapper automatically opens the connection and maps rows to <Student> objects!
            IEnumerable<Student> students = connection.Query<Student>(query);

            foreach (var item in students)
            {
                Console.WriteLine(item.StudentName);
                Console.WriteLine(item.DateOfBirth.ToString("dd MMM yyyy"));
            }
        }

        public Student? Edit(int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);

            string query = @"SELECT [StudentId]
                                  ,[StudentNo]
                                  ,[StudentName]
                                  ,[FatherName]
                                  ,[DateOfBirth]
                                  ,[Gender]
                                  ,[Address]
                                  ,[MobileNo]
                                  ,[DeleteFlag]
                                  ,[CreatedDateTime]
                                  ,[CreatedBy]
                                  ,[ModifiedDateTime]
                                  ,[ModifiedBy] 
                             FROM [dbo].[Tbl_Student] WHERE [StudentId] = @StudentId AND [DeleteFlag] = 0";

            // QueryFirstOrDefault handles matching parameters and returns null if not found
            var student = connection.QueryFirstOrDefault<Student>(query, new { StudentId = id });

            if (student == null)
            {
                Console.WriteLine("Student not found.");
            }

            return student;
        }

        public void Create(Student student)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);

            string query = @"INSERT INTO [dbo].[Tbl_Student] 
                                    ([StudentNo], [StudentName], [FatherName], [DateOfBirth], [Gender], [Address], [MobileNo], [DeleteFlag], [CreatedDateTime], [CreatedBy])
                             VALUES (@StudentNo, @StudentName, @FatherName, @DateOfBirth, @Gender, @Address, @MobileNo, 0, @CreatedDateTime, @CreatedBy)";

            // Set up audit fields dynamically before sending to Dapper
            var parameters = new
            {
                student.StudentNo,
                student.StudentName,
                student.FatherName,
                student.DateOfBirth,
                student.Gender,
                student.Address,
                student.MobileNo,
                CreatedDateTime = DateTime.Now,
                CreatedBy = "admin"
            };

            // connection.Execute returns the number of affected rows
            int result = connection.Execute(query, parameters);
            Console.WriteLine(result > 0 ? "Create successful." : "Create failed.");
        }

        public void Update(Student student)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);

            string query = @"UPDATE [dbo].[Tbl_Student]
                             SET [StudentNo] = @StudentNo,
                                 [StudentName] = @StudentName,
                                 [FatherName] = @FatherName,
                                 [DateOfBirth] = @DateOfBirth,
                                 [Gender] = @Gender,
                                 [Address] = @Address,
                                 [MobileNo] = @MobileNo,
                                 [ModifiedDateTime] = @ModifiedDateTime,
                                 [ModifiedBy] = @ModifiedBy
                             WHERE [StudentId] = @StudentId AND [DeleteFlag] = 0";

            var parameters = new
            {
                student.StudentId,
                student.StudentNo,
                student.StudentName,
                student.FatherName,
                student.DateOfBirth,
                student.Gender,
                student.Address,
                student.MobileNo,
                ModifiedDateTime = DateTime.Now,
                ModifiedBy = "admin"
            };

            int result = connection.Execute(query, parameters);
            Console.WriteLine(result > 0 ? "Update successful." : "Update failed.");
        }

        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);

            string query = @"UPDATE [dbo].[Tbl_Student] 
                             SET [DeleteFlag] = 1,
                                 [ModifiedDateTime] = @ModifiedDateTime,
                                 [ModifiedBy] = @ModifiedBy
                             WHERE [StudentId] = @StudentId";

            int result = connection.Execute(query, new
            {
                StudentId = id,
                ModifiedDateTime = DateTime.Now,
                ModifiedBy = "admin"
            });

            Console.WriteLine(result > 0 ? "Delete successful." : "Delete failed.");
        }
    }
}