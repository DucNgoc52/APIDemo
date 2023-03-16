using System.ComponentModel.DataAnnotations;

namespace WebAPIClone.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Description  { get; set; }
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        [Range(0,100)]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
