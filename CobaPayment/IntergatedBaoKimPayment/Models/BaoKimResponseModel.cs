using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CobastockPayment
{
    public class BaoKimResponseModel
    {
        public BaoKimResponseModel()
        {
            data = new Data();
        }
        public int code { get; set; }
        public List<string> message { get; set; }
        public int count { get; set; }
        Data data { get; set; }
    }
    public class Data
    {
        public Data()
        {
            bank_account = new BankAccount();
        }
        public int order_id { get; set; }
        public string redirect_url { get; set; }
        public string payment_url { get; set; }
        BankAccount bank_account { get; set; }
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
