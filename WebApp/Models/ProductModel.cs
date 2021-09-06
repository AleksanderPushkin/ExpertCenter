using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Database.Entity;

namespace WebApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }
        [Display(Name ="Название продукта")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string Title { get; set; }
        [Display(Name ="Артикул")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string VendorCode { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Value> Values { get; set; } = new List<Value>();
    }

  
}
