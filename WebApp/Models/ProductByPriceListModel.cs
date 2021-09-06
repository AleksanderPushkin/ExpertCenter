using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Database.Entity;

namespace WebApp.Models
{
    public class ProductByPriceListModel
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }
        public string Title { get; set; }
        public string VendorCode { get; set; }
        public PriceList PriceList { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
