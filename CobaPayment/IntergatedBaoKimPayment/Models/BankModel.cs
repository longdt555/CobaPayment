using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntergatedBaoKimPayment.Models
{
    public class BankParamModel
    {
        public string lb_available { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }
    }
    public class BankPaymentModel : ApiResponseBaseModel
    {
        public List<BankPaymentModelDetail> data { get; set; }
    }
    public class BankCardModel : ApiResponseBaseModel
    {
        public List<BankCardModelDetail> data { get; set; }
    }
    public class BankAccountModel : ApiResponseBaseModel
    {
        public List<BankAccountDetail> data { get; set; }
    }
    public class BankModel : ApiResponseBaseModel
    {
        public List<BankDetail> data { get; set; }
    }
    public class BankDetail
    {
        public string id { get; set; }
        public string name { get; set; }
        public string short_name { get; set; }
        public string logo { get; set; }
        public int lb_available { get; set; }
    }

    public class BankPaymentModelDetail : BaseModel
    {
        public string name { get; set; }
        public int bank_id { get; set; }
        public int type { get; set; }
        public string complete_time { get; set; }
        public string bank_name { get; set; }
        public string bank_short_name { get; set; }
        public string bank_logo { get; set; }
    }

    public class BankCardModelDetail
    {
        public string id { get; set; }
        public int user_id { get; set; }
        public int bank_id { get; set; }
        public int deposit_bpm_id { get; set; }
        public int withdraw_bpm_id { get; set; }
        public string card_type { get; set; }
        public string owner_name { get; set; }
        public string short_name { get; set; }
        public string code { get; set; }
        public string cvv_code { get; set; }
        public string token { get; set; }
        public string expiration_date { get; set; }
        public int verification { get; set; }
        public string alias { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class BankAccountDetail : BaseModel
    {
        public int user_id { get; set; }
        public string bank_id { get; set; }
        public string holder { get; set; }
        public string number { get; set; }
        public string branch { get; set; }
        public string province_id { get; set; }
        public string description { get; set; }
        public string is_active { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
    }
}