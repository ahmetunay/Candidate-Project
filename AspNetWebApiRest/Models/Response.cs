using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AspNetWebApiRest.Models
{
    public class Response
    {
        [Key]
        public int referenceNumber { get; set; }
        public bool isError { get; set; }
    }
}