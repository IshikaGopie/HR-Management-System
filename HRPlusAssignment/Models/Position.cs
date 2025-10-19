using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRPlusAssignment.Models
{
    public enum PositionStatus
    {
        Active,
        Inactive,
        Vacant
    }

    public class Position
    {
        [Key]
        public string PositionId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string PositionCode { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string PositionTitle { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey(nameof(Department))]
        public string DepartmentId { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey(nameof(Job))]
        public string JobId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string JobLevel { get; set; } = string.Empty;
        
        [ForeignKey(nameof(ReportsToPosition))]
        public string? ReportsToPositionId { get; set; }
        
        [Required]
        public PositionStatus Status { get; set; }
        
        // Navigation properties
        public virtual Department Department { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
        public virtual Position? ReportsToPosition { get; set; }
        public virtual ICollection<Position> SubordinatePositions { get; set; } = new List<Position>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}