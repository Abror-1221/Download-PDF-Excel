namespace DocumentationWebApp.Areas.Entry.Models
{
    public class TableDataViewModel
    {
        public string SearchId { get; set; }
        public List<(int BookId, string Title, string Category, int Available, int Borrowed)> Books { get; set; }
    }

}
