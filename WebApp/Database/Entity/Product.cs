

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Database.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int PriceListId { get; set; }
        public string Title { get; set; }
        public string VendorCode { get; set; }
        public virtual PriceList PriceList { get; set; }

        public virtual ICollection<Value> Values { get; set; }

    }
}
