using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Database.Entity
{
    public class PriceList
    {
        public PriceList()
        {
            this.Columns = new HashSet<Column>();
            this.Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
