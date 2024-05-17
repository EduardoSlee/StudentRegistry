using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistry.Repositories.Students
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        [MaxLength(20)]
        public string? DocumentType { get; set; }

        [Required]
        [MaxLength(20)]
        public string? DocumentNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public bool Sex { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        
        public string? Nationality { get; set; }

        public string? Photo { get; set; }
    }
}
