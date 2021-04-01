using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApiRest.Models
{
    public class Account
    {
        [Key]
        public int accountNumber { get; set; }
        public string currencyCode { get; set; }
        public decimal balance { get; set; }
       
      
    }
}