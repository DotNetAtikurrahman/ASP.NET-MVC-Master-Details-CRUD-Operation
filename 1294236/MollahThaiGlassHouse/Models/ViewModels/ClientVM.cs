using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MollahThaiGlassHouse.Models.ViewModels
{
    public class ClientVM
    {
        public int CustomerId { get; set; }
        [Display(Name = "Customer Name"), Required]
        public string CustomerName { get; set; }
        public string Picture { get; set; }
        [Display(Name ="Profile Photo")]
        public HttpPostedFileBase PicturFile { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Purchased Date"), Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime PurchaseDate { get; set; }
        public bool IsPaid { get; set; }
        public List<int> ProductList { get; set; } = new List<int>();







    }
}