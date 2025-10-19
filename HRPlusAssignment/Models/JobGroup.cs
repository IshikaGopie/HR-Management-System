using System.ComponentModel.DataAnnotations;

namespace HRPlusAssignment.Models
{
    public class JobGroup
    {
        [Key]
        public string JobGroupId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string JobGroupName { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}