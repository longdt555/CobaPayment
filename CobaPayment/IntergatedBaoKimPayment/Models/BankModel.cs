using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntergatedBaoKimPayment.Models
{
    public class BankModel
    {
        public string lb_available { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }
    }
    public class BankInfoModel
    {
        public int code { get; set; }
        public List<string> message { get; set; }
        public int count { get; set; }
        public List<BankInfoDetail> data { get; set; }
    }
    public class BankInfoDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public int bank_id { get; set; }
        public int type { get; set; }
        public string complete_time { get; set; }
        public string bank_name { get; set; }
        public string bank_short_name { get; set; }
        public string bank_logo { get; set; }
    }
}