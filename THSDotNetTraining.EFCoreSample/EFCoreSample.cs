using System;
using System.Collections.Generic;
using System.Linq;
using THSDotNetTraining.EFCoreSample.DataAccess;

namespace THSDotNetTraining.EFCoreSample.ConsoleApp
{
    public class EfCoreService
    {
        public void Read()
        {
            using var context = new AppDbContext();

            var students = context.TblStudents.Where(x => x.DeleteFlag == false).ToList();

            foreach (var item in students)
            {
                Console.WriteLine(item.StudentName);
                Console.WriteLine(item.DateOfBirth.ToString("dd MMM yyyy"));
            }
        }

        public TblStudent Edit(int id)
        {
            using var context = new AppDbContext();

            var student = context.TblStudents.FirstOrDefault(x => x.StudentId == id && x.DeleteFlag == false);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
            }

            return student!;
        }

        public void Create(TblStudent student)
        {
            using var context = new AppDbContext();

            student.DeleteFlag = false;
            student.CreatedDateTime = DateTime.Now;
            student.CreatedBy = "admin";

            context.TblStudents.Add(student);
            int result = context.SaveChanges();

            Console.WriteLine(result > 0 ? "Create successful." : "Create failed.");
        }

        public void Update(TblStudent student)
        {
            using var context = new AppDbContext();

            // Attaching the student object keeps track of fields altered in your Console program
            context.TblStudents.Update(student);

            student.ModifiedDateTime = DateTime.Now;
            student.ModifiedBy = "admin";

            int result = context.SaveChanges();

            Console.WriteLine(result > 0 ? "Update successful." : "Update failed.");
        }

        public void Delete(int id)
        {
            using var context = new AppDbContext();

            var student = context.TblStudents.FirstOrDefault(x => x.StudentId == id);
            if (student == null)
            {
                Console.WriteLine("Delete failed.");
                return;
            }

            student.DeleteFlag = true;
            student.ModifiedDateTime = DateTime.Now;
            student.ModifiedBy = "admin";

            int result = context.SaveChanges();

            Console.WriteLine(result > 0 ? "Delete successful." : "Delete failed.");
        }
    }
}