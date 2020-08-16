using IntergatedBaoKimPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CobastockPayment
{
    public class OrderModel
    {
    }
    public class OrderParamModel
    {
        public string mrc_order_id { get; set; }
        public int total_amount { get; set; }
        public string description { get; set; }
        public string url_success { get; set; }
        public int merchant_id { get; set; }
        public string url_detail { get; set; }
        public string lang { get; set; }
        public int bpm_id { get; set; }
        public int accept_bank { get; set; }
        public int accept_cc { get; set; }
        public int accept_qrpay { get; set; }
        public int accept_e_wallet { get; set; }
        public string webhooks { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
    }
    public class SendOrderResponseModel<T> : ApiResponseBaseModel
    {
        public List<T> data { get; set; }
    }
    public class SendOrderDataModel
    {
        public int order_id { get; set; }
        public string redirect_url { get; set; }
        public string payment_url { get; set; }
        public string data_qr { get; set; }
        public BankAccount bank_account { get; set; }
    }
    public class BankAccount
    {
        public string acc_name { get; set; }
        public string acc_no { get; set; }
        public string bank_name { get; set; }
        public string branch { get; set; }
        public string amount { get; set; }
    }
}