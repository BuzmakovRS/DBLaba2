using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DBLaba2.Models
{
    [Table("Products", Schema = "public")]
    public class Product
    {

        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        [Column("ProductName"), DisplayName("Продукт")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Материалл")]
        public string Material { get; set; }
        [DisplayName("Длина (мм)")]
        public int Length { get; set; }
        [DisplayName("Ширина (мм)")]
        public int Width { get; set; }
        [DisplayName("Высота (мм)")]
        public int Heigth { get; set; }
        public ICollection<Location> Locations { get; set; }

        public Product()
        {
            Locations = new List<Location>();
        }
    }
}