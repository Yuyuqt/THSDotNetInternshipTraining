using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace THSDotNetTraining.AdoDotNetSample.ConsoleApp
{
    public class AdoDotNetService
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
            connection.Open();

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
                              FROM [Practice].[dbo].[Tbl_Student] WHERE [DeleteFlag] = 0";

            using SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            List<Student> students = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {
                Student item = new Student()
                {
                    StudentId = Convert.ToInt32(row["StudentId"]),
                    StudentNo = row["StudentNo"].ToString()!,
                    StudentName = row["StudentName"].ToString()!,
                    FatherName = row["FatherName"].ToString()!,
                    DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                    Gender = row["Gender"].ToString()!,
                    Address = row["Address"].ToString()!,
                    MobileNo = row["MobileNo"].ToString()!,
                    DeleteFlag = Convert.ToBoolean(row["DeleteFlag"]),
                    CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
                    CreatedBy = row["CreatedBy"].ToString()!,
                    ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
                    ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : row["ModifiedBy"].ToString()
                };
                students.Add(item);

                Console.WriteLine(item.StudentName);
                Console.WriteLine(item.DateOfBirth.ToString("dd MMM yyyy"));
            }
        }

        public Student Edit(int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

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

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Student()
                {
                    StudentId = Convert.ToInt32(reader["StudentId"]),
                    StudentNo = reader["StudentNo"].ToString()!,
                    StudentName = reader["StudentName"].ToString()!,
                    FatherName = reader["FatherName"].ToString()!,
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                    Gender = reader["Gender"].ToString()!,
                    Address = reader["Address"].ToString()!,
                    MobileNo = reader["MobileNo"].ToString()!,
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                    CreatedDateTime = Convert.ToDateTime(reader["CreatedDateTime"]),
                    CreatedBy = reader["CreatedBy"].ToString()!,
                    ModifiedDateTime = reader["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(reader["ModifiedDateTime"]),
                    ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? null : reader["ModifiedBy"].ToString()
                };
            }

            Console.WriteLine("Student not found.");
            return null;
        }

        public void Create(Student student)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Student] 
                                    ([StudentNo], [StudentName], [FatherName], [DateOfBirth], [Gender], [Address], [MobileNo], [DeleteFlag], [CreatedDateTime], [CreatedBy])
                             VALUES 
                                    (@StudentNo, @StudentName, @FatherName, @DateOfBirth, @Gender, @Address, @MobileNo, 0, @CreatedDateTime, @CreatedBy)";

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentNo", student.StudentNo);
            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@MobileNo", student.MobileNo);
            cmd.Parameters.AddWithValue("@CreatedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", "admin");

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result > 0 ? "Create successful." : "Create failed.");
        }

        public void Update(Student student)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

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

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
            cmd.Parameters.AddWithValue("@StudentNo", student.StudentNo);
            cmd.Parameters.AddWithValue("@StudentName", student.StudentName);
            cmd.Parameters.AddWithValue("@FatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", student.Gender);
            cmd.Parameters.AddWithValue("@Address", student.Address);
            cmd.Parameters.AddWithValue("@MobileNo", student.MobileNo);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", "admin");

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result > 0 ? "Update successful." : "Update failed.");
        }

        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Student] 
                             SET [DeleteFlag] = 1,
                                 [ModifiedDateTime] = @ModifiedDateTime,
                                 [ModifiedBy] = @ModifiedBy
                             WHERE [StudentId] = @StudentId";

            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", id);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", "admin");

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result > 0 ? "Delete successful." : "Delete failed.");
        }
    }
}