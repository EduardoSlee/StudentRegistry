namespace StudentRegistry.Services.Students.Models
{
    public class StudentReport
    {
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? EmailAddress { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string? SexDescription { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public string? Nationality { get; set; }
    }
}
