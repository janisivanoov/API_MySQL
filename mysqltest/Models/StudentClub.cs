using System.ComponentModel.DataAnnotations;

namespace mysqltest.Models
{
    public class StudentClub
    {
        public int Id { get; set; }

        [Required]
        public int ClubId { get; set; }

        public Club Club { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}