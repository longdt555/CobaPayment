using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntergatedBaoKimPayment.Models
{
    public class ApiResponseBaseModel
    {
        public int code { get; set; }
        public List<string> message { get; set; }
        public int count { get; set; }
    }
}