namespace SMU_MVC.Models
{
    public class Student
    {
        public Student()
        {
            this.Enrollments = new List<Enrollment>();
        }

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ANumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
