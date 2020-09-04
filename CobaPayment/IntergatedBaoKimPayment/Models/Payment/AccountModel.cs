using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Payment
{
    public class AccountModel<T> : ApiResponseBaseModel<string>
    {
        public List<T> data { get; set; }
    }

    public class AccountDetailModel : BaseModel
    {
        public int user_id { get; set; }
        public int type { get; set; }
        public string balance { get; set; }
        public string freeze_balance { get; set; }
        public int stat { get; set; }
        public int last_act_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}