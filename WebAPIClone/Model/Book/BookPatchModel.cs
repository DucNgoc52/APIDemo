namespace WebAPIClone.Model.Book
{
    public class BookPatchModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
    }
}
