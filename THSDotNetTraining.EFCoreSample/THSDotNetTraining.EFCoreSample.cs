
using System;
using System.Linq;
using THSDotNetTraining.EFCoreSample.DataAccess;

namespace THSDotNetTraining.EFCoreSample.ConsoleApp
{
    public class EfCoreService
    {
        // Read
        public void Read()
        {
            using var context = new AppDbContext();
            Console.WriteLine(" Reading data ");

            var students = context.TblStudents.Where(x => x.DeleteFlag == false).ToList();

            if (!students.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (var student in students)
            {
                Console.WriteLine($"No: {student.StudentNo} - Name: {student.StudentName}");
            }
        }

        // Create
        public void Create(string studentNo, string studentName, string fatherName, DateTime dateOfBirth, string gender, string address, string mobileNo)
        {
            using var context = new AppDbContext();

            var student = new TblStudent
            {
                StudentNo = studentNo,
                StudentName = studentName,
                FatherName = fatherName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Address = address,
                MobileNo = mobileNo,
                DeleteFlag = false
            };

            context.TblStudents.Add(student);
            int result = context.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        // Update
        public void Update(int studentId, string studentName, string address)
        {
            using var context = new AppDbContext();


            var student = context.TblStudents
                .FirstOrDefault(s => s.StudentId == studentId && s.DeleteFlag == false);

            if (student is null)
            {
                Console.WriteLine($"Student with ID {studentId} not found for updating.");
                return;
            }

            student.StudentName = studentName;
            student.Address = address;

            int result = context.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        // Delete 
        public void Delete(int studentId)
        {
            using var context = new AppDbContext();

            var student = context.TblStudents.FirstOrDefault(s => s.StudentId == studentId);

            if (student is null)
            {
                Console.WriteLine($"Student with ID {studentId} not found for deleting.");
                return;
            }

            student.DeleteFlag = true;
            int result = context.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}