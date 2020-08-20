using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntergatedBaoKimPayment.Common
{
    public static class Constant
    {
        // request host
        public const string devHost = "https://sandbox-api.baokim.vn/payment/";
        public const string proHost = "https://api.baokim.vn/payment/";
        // get bank api
        public const string bankApi = "api/v4/bank/list";
        // get bank pay api
        public const string bankPayApi = "api/v4/bpm/list";
        // send order api
        public const string sendOrderApi = "api/v4/order/send";
        // order detail api
        public const string orderDetailApi = "api/v4/order/detail";
        // order list detail api
        public const string orderListDetailApi = "api/v4/order/list";
        // cancel order api
        public const string cancelOrderApi = "api/v4/order/cancel";
        // account detail
        public const string accountDetailApi = "api/v4/account/detail";


    }
    public static class BankPaymentMethod
    {
        public static int BaoKim = 0;
        public static int ATM = 1;
        public static int VISA_Master = 2;
        public static int QRCode = 14;
        public static int QREWallet = 15;
    }
}