
using System.ComponentModel.DataAnnotations;

namespace WebApp.Database.Entity
{
    public class Value
    {
        [Key]
        public int Id { get; set; }
        public int ColumnId { get; set; }
        public string ValueName { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
