using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRPlusAssignment.Models
{
    public class Job
    {
        [Key]
        public string JobId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string JobCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey(nameof(JobGroup))]
        public string JobGroupId { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual JobGroup JobGroup { get; set; } = null!;
        public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
    }
}