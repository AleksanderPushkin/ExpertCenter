using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CreatePriceListWithColumn
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ColumnModel> ColumnModels { get; set; } = new List<ColumnModel>();
    }
}
