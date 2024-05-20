using AutoMapper;
using FluentAssertions;
using Moq;
using StudentRegistry.Repositories.Students;
using StudentRegistry.Services.Students;
using StudentRegistry.Services.Students.Mappings;
using StudentRegistry.Services.Students.Models;
using Xunit;

namespace StudentRegistry.Tests.Services
{
    public class StudentsServiceTests
    {
        private readonly Mock<IStudentsRepository> _studentsRepositoryMock;
        private readonly StudentsService _studentsService;
        private readonly IMapper _mapper;

        public StudentsServiceTests()
        {
            _studentsRepositoryMock = new Mock<IStudentsRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StudentsMappingProfile());
            });
            _mapper = mapperConfiguration.CreateMapper();

            _studentsService = new StudentsService(_studentsRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async void AddStudentAsync_WhenStudentInputGiven_ShouldReturnStudentResult()
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

            var studentResult = await _studentsService.AddStudentAsync(studentInput);

            studentResult.Should().NotBeNull();
            studentResult.Should().BeOfType<StudentResult>();
            studentResult.Should().BeEquivalentTo(_mapper.Map<StudentResult>(_mapper.Map<Student>(studentInput)));
        }

        [Fact]
        public async void GetStudentByIdAsync_WhenStudentIdGivenAndStudentDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Student?)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _studentsService
            .GetStudentByIdAsync(It.IsAny<int>()));

            exception.Message.Should().Be("Student not found.");
        }

        [Fact]
        public async void GetStudentByIdAsync_WhenStudentIdGivenAndStudentExists_ShouldReturnStudentResult()
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

            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(student);

            var studentResult = await _studentsService.GetStudentByIdAsync(It.IsAny<int>());

            studentResult.Should().NotBeNull();
            studentResult.Should().BeOfType<StudentResult>();
            studentResult.Should().BeEquivalentTo(_mapper.Map<StudentResult>(_mapper.Map<Student>(student)));
        }

        [Fact]
        public async void GetAllStudentsAsync_WhenRequestGiven_ShouldReturnStudentResults()
        {
            var students = new Student[]
            {
                new()
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
                },
                new()
                {
                    Id = 2,
                    Name = "NameTest2",
                    LastName = "LastTest2",
                    BirthDate = DateTime.Today.AddDays(-1),
                    Sex = true,
                    DocumentNumber = "DocumentNumberTest2",
                    DocumentType = "DocumentTypeTest2",
                    EmailAddress = "EmailAddressTest2",
                    Nationality = "NationalityTest2",
                    PhoneNumber = "PhoneNumberTest2",
                    Photo = "PhotoTest2",
                    CreateDate = DateTime.Today
                },
            };

            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetAllStudentsAsync())
            .ReturnsAsync(students);

            var studentResults = await _studentsService.GetAllStudentsAsync();

            studentResults.Should().NotBeNull();
            studentResults.Count().Should().Be(studentResults.Count());
            studentResults.Should().BeEquivalentTo(students.Select(student => _mapper.Map<StudentResult>(student)));
        }

        [Fact]
        public async void UpdateStudentAsync_WhenStudentInputAndIdGivenAndStudentDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Student?)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _studentsService
            .UpdateStudentAsync(It.IsAny<int>(), It.IsAny<StudentInput>()));

            exception.Message.Should().Be("Student not found.");
        }

        [Fact]
        public async void UpdateStudentAsync_WhenStudentInputAndIdGivenAndStudentExists_ShouldReturnStudentResult()
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

            var studentInput = new StudentInput()
            {
                Name = "NameTest1",
                LastName = "LastTest1"
            };

            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(student);

            var studentResult = await _studentsService.UpdateStudentAsync(student.Id, studentInput);

            studentResult.Should().NotBeNull();
            studentResult.Should().BeOfType<StudentResult>();
            studentResult.Name.Should().Be(studentInput.Name);
            studentResult.LastName.Should().Be(studentInput.LastName);
            studentResult.BirthDate.Should().Be(student.BirthDate);
            studentResult.Sex.Should().Be(student.Sex);
            studentResult.DocumentNumber.Should().Be(student.DocumentNumber);
            studentResult.DocumentType.Should().Be(student.DocumentType);
            studentResult.EmailAddress.Should().Be(student.EmailAddress);
            studentResult.Nationality.Should().Be(student.Nationality);
            studentResult.PhoneNumber.Should().Be(student.PhoneNumber);
            studentResult.Photo.Should().Be(student.Photo);
        }

        [Fact]
        public async void DeleteStudentAsync_WhenStudentIdGivenAndStudentDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Student?)null);

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _studentsService
            .DeleteStudentAsync(It.IsAny<int>()));

            exception.Message.Should().Be("Student not found.");
        }

        [Fact]
        public async void DeleteStudentAsync_WhenStudentIdGivenAndStudentExists_ShouldDeleteStudentResult()
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
                Photo = "PhotoTest"
            };

            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(student);

            await _studentsService.DeleteStudentAsync(It.IsAny<int>());

            _studentsRepositoryMock.Verify(studentsRepository => studentsRepository
            .DeleteStudentAsync(It.Is<Student>(
                x => x.Id == student.Id
                && x.Name == student.Name
                && x.LastName == student.LastName
                && x.BirthDate == student.BirthDate
                && x.Sex == student.Sex
                && x.DocumentNumber == student.DocumentNumber
                && x.DocumentType == student.DocumentType
                && x.EmailAddress == student.EmailAddress
                && x.Nationality == student.Nationality
                && x.PhoneNumber == student.PhoneNumber
                && x.Photo == student.Photo
                )), Times.Once());
        }

        [Fact]
        public async void GetStudentsExcelReportAsync_WhenCreateDateGiven_ShouldReturnExcelReport()
        {
            var createDate = DateTime.Today;

            _studentsRepositoryMock.Setup(studentRepository => studentRepository.GetStudentsAsync(It.IsAny<DateTime>()))
            .ReturnsAsync(new List<Student>());

            var resultBytes = await _studentsService.GetStudentsExcelReportAsync(createDate);

            resultBytes.Should().NotBeNull();
            resultBytes.Should().BeOfType<byte[]>();

            _studentsRepositoryMock.Verify(studentsRepository => studentsRepository
                .GetStudentsAsync(createDate), Times.Once());
        }
    }
}
