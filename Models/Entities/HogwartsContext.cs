using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class HogwartsContext
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DbSet<Student> _students { get; set; }
        public DbSet<Room> _rooms { get; set; }
    }
}
