
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMU_MVC.Models
{
    public class Course
    {
        public Course()
        {
            this.Enrollments = new List<Enrollment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CRN { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string CourseNumber { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
