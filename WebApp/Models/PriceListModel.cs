
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Database.Entity;

namespace WebApp.Models
{
    public class PriceListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Column> Columns { get; set; }
       
    }
}
