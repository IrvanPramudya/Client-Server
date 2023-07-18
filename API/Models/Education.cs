using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Education
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        [ForeignKey("University")]
        public Guid UniversityGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
