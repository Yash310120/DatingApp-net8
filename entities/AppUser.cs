using System.ComponentModel.DataAnnotations;

namespace DATINGAPP.entities
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; } 
    }
}
