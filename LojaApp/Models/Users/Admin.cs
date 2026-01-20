using System.ComponentModel.DataAnnotations;

namespace LojaApp.Models.Users
{
    public class Admin
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public ApplicationUser User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
