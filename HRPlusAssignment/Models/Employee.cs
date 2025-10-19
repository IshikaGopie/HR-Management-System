using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRPlusAssignment.Models {
    public enum EmployeeStatus {
        Active,
        Inactive,
        OnLeave,
        Terminated
    }

    public class Employee {
        [Key]
        public string EmployeeId { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey(nameof(Position))]
        public string PositionId { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        public EmployeeStatus Status { get; set; }
        
        // Navigation properties
        public virtual Position Position { get; set; } = null!;
    }
}