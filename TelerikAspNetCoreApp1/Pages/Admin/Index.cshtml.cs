using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TelerikAspNetCoreApp1.Data;

namespace TelerikAspNetCoreApp1.Pages.Admin
{
   
    
        public class IndexModel : PageModel
        {
            public static IList<OrderViewModel> orders;

        public delegate string category(int index);



            public void OnGet()
            {
                if (orders == null)
                {
                    orders = new List<OrderViewModel>();
                     DateTime dateTime = new DateTime(2016, 9, 15);
                Random rnd = new Random();
                category c = x => { if (x % 5 == 0) return "Hand Tools"; else if (x % 3 == 0) return "Power Tools"; else return "Indoor Tools"; };

                Enumerable.Range(0, 100).ToList().ForEach(i => orders.Add(new OrderViewModel
                {
                    Price = Math.Round(rnd.NextDouble() * 100 + i, 2),
                    OrderID = i * 10,
                    //  ValidForm = dateTime.AddDays(i % 7),
                    ValidForm = dateTime.AddDays(i%7).ToString("dd.MM.yyy"),
                    Name = "Product " + i,                   
                    Category =c(i),
                    Quantity = (int)(rnd.NextDouble() *10) +i
                    }));

                }
            }

            public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
            {
                return new JsonResult(orders.ToDataSourceResult(request));
            }

            public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
            {
                order.OrderID = orders.Count + 2;
                orders.Add(order);

                return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
            }

            public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
            {
                orders.Where(x => x.OrderID == order.OrderID).Select(x => order);

                return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
            }

            public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
            {
                orders.Remove(orders.First(x => x.OrderID == order.OrderID));

                return new JsonResult(new[] { order }.ToDataSourceResult(request, ModelState));
            }
        }
    
}