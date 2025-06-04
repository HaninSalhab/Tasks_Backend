using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Domain.Users.Models;

namespace TaskManagement.Domain.Tasks.Models
{
    [Table("Tasks")]
    [Index(nameof(UserId))]
    public class ProjectTask
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
       
        public void Complete(long userId)
        {
            Completed = true;
            ModifiedAt = DateTime.UtcNow;
            UserId = userId;
        }
    }
}
