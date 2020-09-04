using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Payment
{
    public class ApiResponseBaseModel<T>
    {
        public int code { get; set; }
        public T message { get; set; }
        public int count { get; set; }
    }
    public class MessageModel
    {
        public MessageModel()
        {
            mrc_order_id = new List<string>();
            total_amount = new List<string>();
            description = new List<string>();
            url_success = new List<string>();
            merchant_id = new List<string>();
            url_detail = new List<string>();
            lang = new List<string>();
            bpm_id = new List<string>();
            accept_bank = new List<string>();
            accept_cc = new List<string>();
            accept_qrpay = new List<string>();
            accept_e_wallet = new List<string>();
            webhooks = new List<string>();
            customer_email = new List<string>();
            customer_phone = new List<string>();
            customer_name = new List<string>();
            customer_address = new List<string>();
        }
        public List<string> mrc_order_id { get; set; }
        public List<string> total_amount { get; set; }
        public List<string> description { get; set; }
        public List<string> url_success { get; set; }
        public List<string> merchant_id { get; set; }
        public List<string> url_detail { get; set; }
        public List<string> lang { get; set; }
        public List<string> bpm_id { get; set; }
        public List<string> accept_bank { get; set; }
        public List<string> accept_cc { get; set; }
        public List<string> accept_qrpay { get; set; }
        public List<string> accept_e_wallet { get; set; }
        public List<string> webhooks { get; set; }
        public List<string> customer_email { get; set; }
        public List<string> customer_phone { get; set; }
        public List<string> customer_name { get; set; }
        public List<string> customer_address { get; set; }
    }
}