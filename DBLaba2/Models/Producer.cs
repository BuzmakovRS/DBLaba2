using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DBLaba2.Models
{
    [Table("Producers", Schema = "public")]
    public class Producer
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Column("ProducerName"), DisplayName("Производитель")]
        public string Name { get; set; }
        [DisplayName("Город")]
        public string City { get; set; }
        public ICollection<Location> Locations { get; set; }

        public Producer()
        {
            Locations = new List<Location>();
        }
    }
}