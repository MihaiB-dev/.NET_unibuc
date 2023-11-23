using System.ComponentModel.DataAnnotations;

namespace lab1_8.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; } 
        public string CategoryName { get; set; }
    }
}
