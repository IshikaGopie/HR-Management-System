using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual Department Department { get; set; } = null!;
        [JsonIgnore]
        public virtual Job Job { get; set; } = null!;
        [JsonIgnore]
        public virtual Position? ReportsToPosition { get; set; }
        [JsonIgnore]
        public virtual ICollection<Position> SubordinatePositions { get; set; } = new List<Position>();
        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}