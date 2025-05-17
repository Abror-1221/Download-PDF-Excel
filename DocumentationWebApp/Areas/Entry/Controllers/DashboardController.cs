using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using DocumentationWebApp.Areas.Entry.Models;

namespace DocumentationWebApp.Areas.Entry.Controllers
{
    [Area("Entry")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult TableData(string searchId)
        {
            var dummyBooks = GetDummyBooks();

            if (!string.IsNullOrEmpty(searchId))
            {
                dummyBooks = dummyBooks
                    .Where(b => b.BookId.ToString().StartsWith(searchId))
                    .ToList();
            }

            var model = new TableDataViewModel
            {
                Books = dummyBooks,
                SearchId = searchId
            };

            return View(model);
        }

        public IActionResult ExportPdf(string searchId)
        {
            var dummyBooks = GetDummyBooks();

            if (!string.IsNullOrEmpty(searchId))
            {
                dummyBooks = dummyBooks
                    .Where(b => b.BookId.ToString().StartsWith(searchId))
                    .ToList();
            }

            var model = new TableDataViewModel
            {
                Books = dummyBooks,
                SearchId = searchId
            };

            return new ViewAsPdf("TableDataPdf", model)
            {
                FileName = $"BookData_{DateTime.Now:dd-MM-yyyy_HH:mm:ss}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }

        public IActionResult ExportExcel(string searchId)
        {
            var books = GetDummyBooks();

            if (!string.IsNullOrEmpty(searchId))
            {
                books = books
                    .Where(b => b.BookId.ToString().StartsWith(searchId))
                    .ToList();
            }

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Book Data");

            // Header
            worksheet.Cell(1, 1).Value = "BookId";
            worksheet.Cell(1, 2).Value = "Book Title";
            worksheet.Cell(1, 3).Value = "Category";
            worksheet.Cell(1, 4).Value = "Available";
            worksheet.Cell(1, 5).Value = "Borrowed";

            // Data rows
            for (int i = 0; i < books.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = books[i].BookId;
                worksheet.Cell(i + 2, 2).Value = books[i].Title;
                worksheet.Cell(i + 2, 3).Value = books[i].Category;
                worksheet.Cell(i + 2, 4).Value = books[i].Available;
                worksheet.Cell(i + 2, 5).Value = books[i].Borrowed;
            }

            // Auto fit columns
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var timestamp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            var fileName = $"BookData_{timestamp}.xlsx";

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        private List<(int BookId, string Title, string Category, int Available, int Borrowed)> GetDummyBooks()
        {
            return new List<(int, string, string, int, int)>
            {
                (101, "Clean Code", "Programming", 5, 3),
                (102, "The Pragmatic Programmer", "Programming", 2, 4),
                (201, "Atomic Habits", "Self Development", 10, 6),
                (301, "Design Patterns", "Software Engineering", 1, 1),
                (401, "Deep Work", "Productivity", 3, 2),
                (103, "Refactoring", "Programming", 4, 2),
                (501, "The Lean Startup", "Business", 7, 3),
                (601, "Eloquent JavaScript", "Web Development", 6, 4),
                (701, "Grit", "Psychology", 8, 5),
                (801, "Domain-Driven Design", "Software Architecture", 2, 1),
                (202, "How to Read Minds", "Self Development", 1, 1),
                (203, "Master of Communicate", "Self Development", 2, 1)
            };
        }

    }
}
