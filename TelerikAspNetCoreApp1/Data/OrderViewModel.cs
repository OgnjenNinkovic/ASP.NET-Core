using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikAspNetCoreApp1.Data
{
    public class OrderViewModel
    {
        [Key]
        public int OrderID
        {
            get;
            set;
        }

        [Required]
        public double? Price
        {
            get;
            set;
        }

        public int? Quantity { get; set; }

        //public DateTime? ValidForm
        //{
        //    get;
        //    set;
        //}

        public string ValidForm
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}