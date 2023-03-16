using System.ComponentModel.DataAnnotations;

namespace WebAPIClone.Data
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
