using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LinkModel
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }

        public int ColumnId { get; set; }
        public virtual ColumnModel Column { get; set; }
        public virtual PriceListModel PriceList { get; set; }
    }
}
