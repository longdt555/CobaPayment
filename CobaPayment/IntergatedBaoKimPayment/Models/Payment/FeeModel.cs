using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Payment
{
    public class FeeModel<T> : ApiResponseBaseModel<string>
    {
        public List<T> data { get; set; }
    }
    public class FeeDetailModel
    {
        public int fee_amount { get; set; }
        public int fee_payer { get; set; }
    }
}