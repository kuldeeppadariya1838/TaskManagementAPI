using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.DTOs.Tasks
{
    public class UpdateTaskDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public string Priority { get; set; } // Low, Medium, High

        [Required]
        public string Status { get; set; } // Pending, In Progress, Completed
    }
}
