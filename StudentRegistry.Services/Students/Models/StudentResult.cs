namespace StudentRegistry.Services.Students.Models
{
    public class StudentResult
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Sex { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Nationality { get; set; }

        public string? Photo { get; set; }

        public string? CreateDate { get; set; }
    }
}
