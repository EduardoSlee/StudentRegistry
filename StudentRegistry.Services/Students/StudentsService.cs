using AutoMapper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using StudentRegistry.Repositories.Students;
using StudentRegistry.Services.Students.Models;

namespace StudentRegistry.Services.Students
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IMapper _mapper;

        public StudentsService(IStudentsRepository studentsRepository, IMapper mapper)
        {
            _studentsRepository = studentsRepository;
            _mapper = mapper;
        }

        public async Task<StudentResult> AddStudentAsync(StudentInput studentInput)
        {
            var student = _mapper.Map<Student>(studentInput);

            await _studentsRepository.AddStudentAsync(student);

            return _mapper.Map<StudentResult>(student);
        }

        public async Task<StudentResult?> GetStudentByIdAsync(int id)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student is null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            return _mapper.Map<StudentResult>(student);
        }

        public async Task<StudentResult> UpdateStudentAsync(int id, StudentInput studentInput)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student is null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            _mapper.Map(studentInput, student);

            await _studentsRepository.UpdateStudentAsync(student);

            return _mapper.Map<StudentResult>(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _studentsRepository.GetStudentByIdAsync(id);

            if (student is null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            await _studentsRepository.DeleteStudentAsync(student);
        }

        public async Task<byte[]> GetStudentsExcelReportAsync(DateTime? createDate)
        {
            var students = await _studentsRepository.GetStudentsAsync(createDate);
            var studentsReport = students.Select(student => _mapper.Map<StudentReport>(student));

            using var memStream = new MemoryStream();
            using var spreadsheetDocument = SpreadsheetDocument.Create(memStream, SpreadsheetDocumentType.Workbook);

            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Students" };
            sheets.Append(sheet);

            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            var headerRow = new Row();
            headerRow.Append(
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Name") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Last Name") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Email Address") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Document Type") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Document Number") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Birth Date") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Sex Description") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Phone Number") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Create Date") },
                new Cell() { DataType = CellValues.String, CellValue = new CellValue("Nationality") }
            );
            sheetData.AppendChild(headerRow);

            foreach (var student in studentsReport)
            {
                var dataRow = new Row();
                dataRow.Append(
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.Name) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.LastName) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.EmailAddress) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.DocumentType) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.DocumentNumber) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.BirthDate.ToString("yyyy-MM-dd")) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.SexDescription) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.PhoneNumber) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.CreateDate.ToString("yyyy-MM-dd")) },
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(student.Nationality) }
                );
                sheetData.AppendChild(dataRow);
            }

            workbookPart.Workbook.Save();
            spreadsheetDocument.Dispose();

            return memStream.ToArray();
        }
    }
}
