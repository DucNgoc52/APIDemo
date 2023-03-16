using System.ComponentModel.DataAnnotations;

namespace WebAPIClone.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
