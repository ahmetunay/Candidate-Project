using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetWebApiRest.Models
{
    public class MoneyT
    {
        public int Sender { get; set; }
        public int Reciver { get; set; }
        public decimal Amount { get; set; }

    }
}