using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THSDotNetTraining.AdoDotNetSample
{
    public class Student
    {
        public int StudentId { get; set; }

        public string StudentNo { get; set; } = null!;

        public string StudentName { get; set; } = null!;

        public string FatherName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string MobileNo { get; set; } = null!;

        public bool DeleteFlag { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedDateTime { get; set; }

        public string? ModifiedBy { get; set; }
    }
}