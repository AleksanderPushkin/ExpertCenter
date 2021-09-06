
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Database.Entity
{
    public class Column
    {
        public Column()
        {
            this.PriceLists = new HashSet<PriceList>();
        }
        [Key]
        public int Id { get; set; }
        public string Titile { get; set; }

        public ColumnTypeEnum TypeId { get; set; }

        public virtual ICollection<PriceList> PriceLists { get; set; }
    } 

    public enum ColumnTypeEnum {
    
        Text,

        Area,

        DateTime,
        
        Int,

        Boolean
    
    }
}
