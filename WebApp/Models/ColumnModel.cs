using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Database.Entity;

namespace WebApp.Models
{
    public class ColumnModel
    { 

     public int Id { get; set; }
    public string Titile { get; set; }

    public ColumnTypeEnum TypeId { get; set; }


    }

 
}
