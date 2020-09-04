using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Payment
{
    public class OrderModel<T> : ApiResponseBaseModel<List<string>>
    {
        public T data { get; set; }
    }
    public class OrderModelDetail : BaseModel
    {
        public int user_id { get; set; }
        public string mrc_order_id { get; set; }
        public string txn_id { get; set; } // txn_id (mã giao dịch thanh toán) có giá trị
        public string ref_no { get; set; }
        public string deposit_id { get; set; }
        public int merchant_id { get; set; }
        public string total_amount { get; set; }
        public string shipping_fee { get; set; }
        public string tax_fee { get; set; }
        public string mrc_fee { get; set; }
        public string description { get; set; }
        public string items { get; set; }
        public string url_success { get; set; }
        public string url_cancel { get; set; }
        public string url_detail { get; set; }
        //Danh sách trạng thái đơn hàng "stat":
        //'p': processing(đang xử lý)
        //'c': completed(hoàn thành, đã thanh toán thành công)
        //'r': reviewing(khách đã thanh toán, nhưng tiền chờ duyệt, áp dụng đối với 1 số thẻ visa, rất ít)
        //'d': destructed(hủy thanh toán, hủy duyệt tiền thanh toán visa)
        public string stat { get; set; }
        public string payment_version { get; set; }
        public string lang { get; set; }
        public string bpm_id { get; set; }
        public bool accept_qrpay { get; set; }
        public bool accept_bank { get; set; }
        public bool accept_cc { get; set; }
        public bool accept_e_wallet { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string webhooks { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
        public string customer_address { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string redirect_url { get; set; }
        public string payment_url { get; set; }


    }

    public class OrderParamModel
    {
        public string mrc_order_id { get; set; }
        public double total_amount { get; set; }
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
    public class SendOrderResponseModel<T> : ApiResponseBaseModel<MessageModel>
    {
        public T data { get; set; }
    }
    public class SendOrderResponseModelv2<T> : ApiResponseBaseModel<List<String>>
    {
        public T data { get; set; }
    }
    public class SendOrderDataModel
    {
        public double order_id { get; set; }
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