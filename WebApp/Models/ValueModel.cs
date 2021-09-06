using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ValueModel
    {
        public string Id { get; set; }
        public int PriceId { get; set; }
        public string Title { get; set; }
        public string VendorCode { get; set; }
        public virtual PriceListModel PriceList { get; set; }
    }
}
