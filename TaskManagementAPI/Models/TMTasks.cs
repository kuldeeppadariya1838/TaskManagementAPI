using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models
{
    public class TMTasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; } // Low, Medium, High

        [Required]
        public string Status { get; set; } // Pending, In Progress, Completed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int UserId { get; set; }
        public TMUsers TMUsers { get; set; }
    }
}
