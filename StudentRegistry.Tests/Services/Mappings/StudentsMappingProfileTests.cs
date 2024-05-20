using AutoMapper;
using FluentAssertions;
using StudentRegistry.Repositories.Students;
using StudentRegistry.Services.Students.Mappings;
using StudentRegistry.Services.Students.Models;
using Xunit;

namespace StudentRegistry.Tests.Services.Mappings
{
    public class StudentsMappingProfileTests
    {
        private readonly IMapper _mapper;

        public StudentsMappingProfileTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StudentsMappingProfile());
            });

            _mapper = mapperConfiguration.CreateMapper();
        }

        [Fact]
        public void StudentsMappingProfile_WhenStudentGiven_ShouldReturnStudentResult()
        {
            var student = new Student()
            {
                Id = 1,
                Name = "NameTest",
                LastName = "LastTest",
                BirthDate = DateTime.Today,
                Sex = true,
                DocumentNumber = "DocumentNumberTest",
                DocumentType = "DocumentTypeTest",
                EmailAddress = "EmailAddressTest",
                Nationality = "NationalityTest",
                PhoneNumber = "PhoneNumberTest",
                Photo = "PhotoTest",
                CreateDate = DateTime.Today
            };

            var studentResult = _mapper.Map<StudentResult>(student);

            studentResult.Should().NotBeNull();
            studentResult.Should().BeOfType<StudentResult>();
            studentResult.Id.Should().Be(student.Id);
            studentResult.Name.Should().Be(student.Name);
            studentResult.LastName.Should().Be(student.LastName);
            studentResult.BirthDate.Should().Be(student.BirthDate);
            studentResult.Sex.Should().Be(student.Sex);
            studentResult.DocumentNumber.Should().Be(student.DocumentNumber);
            studentResult.DocumentType.Should().Be(student.DocumentType);
            studentResult.EmailAddress.Should().Be(student.EmailAddress);
            studentResult.Nationality.Should().Be(student.Nationality);
            studentResult.PhoneNumber.Should().Be(student.PhoneNumber);
            studentResult.Photo.Should().Be(student.Photo);
            studentResult.CreateDate.Should().Be(student.CreateDate.ToString("dd/MM/yyyy"));
        }

        [Fact]
        public void StudentsMappingProfile_WhenStudentInputGiven_ShouldReturnStudent()
        {
            var studentInput = new StudentInput()
            {
                Name = "NameTest",
                LastName = "LastTest",
                BirthDate = DateTime.Today,
                Sex = true,
                DocumentNumber = "DocumentNumberTest",
                DocumentType = "DocumentTypeTest",
                EmailAddress = "EmailAddressTest",
                Nationality = "NationalityTest",
                PhoneNumber = "PhoneNumberTest",
                Photo = "PhotoTest"
            };

            var student = _mapper.Map<Student>(studentInput);

            student.Should().NotBeNull();
            student.Should().BeOfType<Student>();
            student.Name.Should().Be(studentInput.Name);
            student.LastName.Should().Be(studentInput.LastName);
            student.BirthDate.Should().Be(studentInput.BirthDate);
            student.Sex.Should().Be(studentInput.Sex);
            student.DocumentNumber.Should().Be(studentInput.DocumentNumber);
            student.DocumentType.Should().Be(studentInput.DocumentType);
            student.EmailAddress.Should().Be(studentInput.EmailAddress);
            student.Nationality.Should().Be(studentInput.Nationality);
            student.PhoneNumber.Should().Be(studentInput.PhoneNumber);
            student.Photo.Should().Be(studentInput.Photo);
        }

        [Fact]
        public void StudentsMappingProfile_WhenStudentGiven_ShouldReturnStudentReport()
        {
            var student = new Student()
            {
                Id = 1,
                Name = "NameTest",
                LastName = "LastTest",
                BirthDate = DateTime.Today,
                Sex = true,
                DocumentNumber = "DocumentNumberTest",
                DocumentType = "DocumentTypeTest",
                EmailAddress = "EmailAddressTest",
                Nationality = "NationalityTest",
                PhoneNumber = "PhoneNumberTest",
                Photo = "PhotoTest",
                CreateDate = DateTime.Today
            };

            var studentReport = _mapper.Map<StudentReport>(student);

            studentReport.Should().NotBeNull();
            studentReport.Should().BeOfType<StudentReport>();
            studentReport.Name.Should().Be(student.Name);
            studentReport.LastName.Should().Be(student.LastName);
            studentReport.BirthDate.Should().Be(student.BirthDate);
            studentReport.SexDescription.Should().Be("Male");
            studentReport.DocumentNumber.Should().Be(student.DocumentNumber);
            studentReport.DocumentType.Should().Be(student.DocumentType);
            studentReport.EmailAddress.Should().Be(student.EmailAddress);
            studentReport.Nationality.Should().Be(student.Nationality);
            studentReport.PhoneNumber.Should().Be(student.PhoneNumber);
            studentReport.CreateDate.Should().Be(student.CreateDate);
        }
    }
}
