using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class Lectures
    {
        [Key]
        public int IdOfLecture { get; set; }
        public string NameOfLecture { get; set; }
        public string Lecturer { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public string Enrolled { get; set; }
        public Lectures()
        {

        }
    }
}
