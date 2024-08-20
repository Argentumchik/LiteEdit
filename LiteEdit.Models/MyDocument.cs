namespace LiteEdit.Models
{
    public class MyDocument
    {
        public string? FileName { get; set; }
        public string? Content { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

}
