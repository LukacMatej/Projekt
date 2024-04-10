using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [AllowNull]
        public string Enrolled { get; set; }
        public Lectures()
        {

        }
    }
}
