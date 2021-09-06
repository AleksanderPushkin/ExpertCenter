using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Database.Entity
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public int PriceListId { get; set; }

        public int ColumnId { get; set; }
        public virtual Column Column { get; set; }
        public virtual PriceList PriceList { get; set; }

    }
}
