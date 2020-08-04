using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobastockPayment
{
    public class OrderInfoModel
    {
        public string mrc_order_id { get; set; }
        public int total_amount { get; set; }
        public string description { get; set; }
        public string url_success { get; set; }
        public int merchant_id { get; set; }
        public string url_detail { get; set; }
        public string lang { get; set; }
        public int bpm_id { get; set; }
        public bool accept_bank { get; set; }
        public bool accept_cc { get; set; }
        public bool accept_qrpay { get; set; }
        public bool accept_e_wallet { get; set; }
        public string webhooks { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
    }
}
