using System.ComponentModel.DataAnnotations;

namespace HRPlusAssignment.Models
{
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string DepartmentCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
        
        // Navigation properties
        public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
    }
}