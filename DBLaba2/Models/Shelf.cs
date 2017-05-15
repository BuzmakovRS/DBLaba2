using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DBLaba2.Models
{
    [Table("Shelves", Schema = "public")]
    public class Shelf
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("ShelfName"), DisplayName("Стеллаж")]
        public string Name { get; set; }
        [DisplayName("Расположение")]
        public string Position { get; set; }
        public ICollection<Location> Locations { get; set; }

        public Shelf()
        {
            Locations = new List<Location>();
        }
    }
}